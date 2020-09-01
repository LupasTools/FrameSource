// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// IFrameSource_Events.cs : 30.8.2020
// MIT license

using System;

namespace Lupas.FrameSource
{
    public delegate void NewFrameEventHandler(object sender, NewFrameEventArgs e);
    public class NewFrameEventArgs : EventArgs
    {
        public readonly double Fps;

        public NewFrameEventArgs(double fps)
        {
            Fps = fps;
        }
    }

    public delegate void TaskExceptionHandler(object sender, ExceptionEventArgs e);

    public class ExceptionEventArgs : EventArgs
    {
        public readonly string Message;
        public readonly Exception Exception;
        public ExceptionEventArgs(Exception ex, string message = "")
        {
            Message = message;
            Exception = ex;
        }
    }
}
