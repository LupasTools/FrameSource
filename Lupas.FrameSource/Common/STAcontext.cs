// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// STAcontext.cs : 6.9.2020
// MIT license

using System;
using System.Collections.Generic;
using System.Threading;

namespace Lupas.FrameSource.Common
{
    /// <summary>
    /// Realisation of SynchronizationContext with safe queue
    /// </summary>
    public class STAcontext : SynchronizationContext, IDisposable
    {
        private BlockingQueue<SendOrPostCallbackItem> queue;
        private StaThread staThread;
        public STAcontext()
           : base()
        {
            queue = new BlockingQueue<SendOrPostCallbackItem>();
            staThread = new StaThread(queue);
            staThread.Start();
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            SendOrPostCallbackItem item = new SendOrPostCallbackItem(d, state, ExecutionType.Send);
            queue.Enqueue(item);

            // wait for execution
            item.ExecutionCompleteWaitHandle.WaitOne();

            if (item.ExecutedWithException)
                throw item.Exception;
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            // queue the item and don't wait for its execution
            SendOrPostCallbackItem item = new SendOrPostCallbackItem(d, state, ExecutionType.Post);
            queue.Enqueue(item);
        }

        public void Dispose()
        {
            staThread.Stop();
        }
    }
    internal interface IQueueReader<T> : IDisposable
    {
        T Dequeue();
        void ReleaseReader();
    }
    internal interface IQueueWriter<T> : IDisposable
    {
        void Enqueue(T data);
    }
    internal class BlockingQueue<T> : IQueueReader<T>,
                                     IQueueWriter<T>, IDisposable
    {
        private Queue<T> mQueue = new Queue<T>();
        private Semaphore mSemaphore = new Semaphore(0, int.MaxValue);
        private ManualResetEvent mKillThread = new ManualResetEvent(false);
        private WaitHandle[] mWaitHandles;

        public BlockingQueue()
        {
            mWaitHandles = new WaitHandle[2] { mSemaphore, mKillThread };
        }
        public void Enqueue(T data)
        {
            lock (mQueue) mQueue.Enqueue(data);
            mSemaphore.Release();
        }

        public T Dequeue()
        {
            WaitHandle.WaitAny(mWaitHandles);
            lock (mQueue)
            {
                if (mQueue.Count > 0)
                    return mQueue.Dequeue();
            }
            return default(T);
        }

        public void ReleaseReader()
        {
            mKillThread.Set();
        }

        void IDisposable.Dispose()
        {
            if (mSemaphore != null)
            {
                mSemaphore.Close();
                mQueue.Clear();
                mSemaphore = null;
            }
        }
    }
    internal enum ExecutionType
    {
        Post,
        Send
    }
    internal class SendOrPostCallbackItem
    {
        object state;
        private ExecutionType exeType;
        SendOrPostCallback method;
        ManualResetEvent asyncWaitHandle = new ManualResetEvent(false);
        Exception mException = null;

        internal SendOrPostCallbackItem(SendOrPostCallback callback,
           object state, ExecutionType type)
        {
            method = callback;
            this.state = state;
            exeType = type;
        }

        internal Exception Exception
        {
            get { return mException; }
        }

        internal bool ExecutedWithException
        {
            get { return mException != null; }
        }

        internal void Execute()
        {
            Send();
        }

        internal void Send()
        {
            try
            {
                method(state);
            }
            catch (Exception e)
            {
                mException = e;
            }
            finally
            {
                asyncWaitHandle.Set();
            }
        }
        /// <summary />
        /// Unhandled exceptions will terminate the STA thread
        /// </summary />
        internal void Post()
        {
            method(state);
        }

        internal WaitHandle ExecutionCompleteWaitHandle
        {
            get { return asyncWaitHandle; }
        }
    }
    internal class StaThread
    {
        private Thread mStaThread;
        private IQueueReader<SendOrPostCallbackItem> mQueueConsumer;

        private ManualResetEvent mStopEvent = new ManualResetEvent(false);


        internal StaThread(IQueueReader<SendOrPostCallbackItem> reader)
        {
            mQueueConsumer = reader;
            mStaThread = new Thread(Run);
            mStaThread.Name = "Board Execution Thread";
            mStaThread.SetApartmentState(ApartmentState.STA);
        }

        internal void Start()
        {
            mStaThread.Start();
        }


        internal void Join()
        {
            mStaThread.Join();
        }

        private void Run()
        {

            while (true)
            {
                bool stop = mStopEvent.WaitOne(0);
                if (stop)
                {
                    break;
                }

                SendOrPostCallbackItem workItem = mQueueConsumer.Dequeue();
                if (workItem != null)
                    workItem.Execute();
            }
        }

        internal void Stop()
        {
            mStopEvent.Set();
            mQueueConsumer.ReleaseReader();
            mStaThread.Join();
            mQueueConsumer.Dispose();
        }
    }
}
