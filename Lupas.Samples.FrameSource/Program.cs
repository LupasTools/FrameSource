// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// Program.cs : 30.8.2020
// MIT license

using Lupas.FrameSource;
using Lupas.FrameSource.MediaFoundation;
using System;
using System.Windows.Forms;

namespace Lupas.Samples.FrameSource
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var source = JsonFile.Deserialize<MF_LiveFrameSource>();
            Application.Run(new FrameSourceSampleForm(source, new UnmanagedFrameData()));
        }
    }
}
