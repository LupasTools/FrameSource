// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// IFrameSource.cs : 30.8.2020
// MIT license

using System;

namespace Lupas.FrameSource
{
    public interface IFrameSource : IDisposable
    {
        /// <summary>
        /// New frame notification
        /// </summary>
        event NewFrameEventHandler OnNewFrame;
        /// <summary>Exceptions notification</summary>
        /// <remarks>
        /// Get frame provider exception object in the event args if subscribed 
        /// Else, provider exceptions will be thrown to Live() of Freeze() caller
        /// </remarks>
        event TaskExceptionHandler OnWorkerException;
        /// <summary>
        /// Start new (or resume current) frame provider task on worker thread
        /// </summary>
        void Live();
        /// <summary>
        /// Freeze current frame, i.e. pause frame provider
        /// </summary>
        /// <remarks>Start new (or resume current) frame provider task on worker thread and then wait for an actual frame on the caller thread</remarks>
        void Freeze();
        /// <summary>
        /// Copy last frame to destination container in a safe way
        /// </summary>
        /// <param name="destination">container to where frame will be copied</param>
        void CopyLastFrame(IFrameData destination);
        /// <summary>
        /// Manage frame source properties
        /// </summary>
        void ShowProperties();
    }
}
