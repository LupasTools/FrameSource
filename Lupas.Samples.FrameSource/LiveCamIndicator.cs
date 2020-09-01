using System;

namespace Lupas.Samples.FrameSource
{
    public class LiveCamIndicator
    {
        public enum States
        {
            PAUSE,
            LIVE,
            ERROR,
            STOP
        }
        bool curLiveState = false;
        public bool Live => curLiveState;
        public event EventHandler ErrorStateDetected;
        readonly System.Windows.Forms.Timer watchdog = new System.Windows.Forms.Timer();

        public LiveCamIndicator()
        {
            watchdog.Interval = 1000;
            watchdog.Tick += (obj, args)=> {
                curLiveState = false;
                watchdog.Stop();
                ErrorStateDetected?.Invoke(this, new EventArgs());
            };
        }

        public void Change(States newState)
        {
            switch(newState)
            {
                case States.ERROR:
                case States.PAUSE:
                case States.STOP:
                    curLiveState = false;
                    watchdog.Stop();
                    watchdog.Enabled = false;
                    break;
                case States.LIVE:
                    curLiveState = true;
                    watchdog.Enabled = true;
                    watchdog.Interval = 1000;
                    watchdog.Start();
                    break;
            }
        }
    }
}
