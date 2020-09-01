// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// PerfomanceComponent.cs : 30.8.2020
// MIT license

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lupas.Samples.FrameSource
{
    public class PerfomanceComponent : Timer
    {
        public event EventHandler OnPerfomanceMeasured;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new public event EventHandler Tick = delegate { };

        PerformanceCounter cpu = null;
        PerformanceCounter ram = null;

        double cpuUsage = double.NaN;
        double ramUsage = double.NaN;

        string getProcessName()
        {
            var process = Process.GetCurrentProcess();
            foreach (var instance in new PerformanceCounterCategory("Process").GetInstanceNames())
                if (instance.StartsWith(process.ProcessName))
                    using (var processId = new PerformanceCounter("Process", "ID Process", instance, true))
                        if (process.Id == (int)processId.RawValue)
                            return instance;
            return string.Empty;
        }

        protected override void OnTick(EventArgs e)
        {
            if (cpu == null) Init();
            cpuUsage = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 0);
            ramUsage = Math.Round(ram.NextValue() / 1024 / 1024, 0);
            OnPerfomanceMeasured?.Invoke(this, e);
        }

        private void Init()
        {
            var processName = getProcessName();
            cpu = new PerformanceCounter("Process", "% Processor Time", processName, true);
            ram = new PerformanceCounter("Process", "Private Bytes", processName, true);
        }

        public override string ToString()
        {
            return $"CPU: {cpuUsage,2} %  RAM: {ramUsage} MB";
        }
    }
}
