// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// FrameSource_TDD.cs : 30.8.2020
// MIT license 

using Lupas.FrameSource;
using Lupas.FrameSource.MediaFoundation;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Lupas.Tests.FrameSource
{
    //    [TestFixture(typeof(DS_FrameSource))]
    [TestFixture(typeof(MF_LiveFrameSource))]
    class TDD<T> where T : IFrameSource, new()
    {
        private T frameSource;
        [SetUp] public void CreateFrameSource() { frameSource = new T(); frameSource.OnWorkerException += FrameSource_OnInnerException; }
        private void FrameSource_OnInnerException(object sender, ExceptionEventArgs e)
        {
            Assert.Fail(e.Message);
        }
        [TearDown] public void ReleaseFrameSource() { frameSource?.Dispose(); }
        [Test]
        public void StartLive()
        {
            int counter = 0;
            int timeMS = 1000;
            frameSource.Live();
            frameSource.OnNewFrame += (t, e) => { counter++; };
            Thread.Sleep(timeMS);
            int finalFrames = counter;
            Assert.IsTrue(finalFrames > 0, $"frames catched: [{finalFrames}] for [{timeMS}] ms");
        }
        [Test]
        public void GetSingleFrame()
        {
            frameSource.Freeze();
        }
        [Test]
        public void SaveSingleFrame()
        {
            frameSource.Freeze();
            ManagedFrameData fd = new ManagedFrameData();
            frameSource.CopyLastFrame(fd);
            var filePath = $"{AssemblyDirectory}/testbmp.png";
            using (var bmp = fd.ToBitmap()) bmp.Save(filePath, ImageFormat.Png);
            File.Delete(filePath);
        }
        [Test]
        public void SaveSingleFrameUnmanaged()
        {
            frameSource.Freeze();
            UnmanagedFrameData fd = new UnmanagedFrameData();
            frameSource.CopyLastFrame(fd);
            var filePath = $"{AssemblyDirectory}/testbmp.png";
            using (var bmp = fd.ToBitmap()) bmp.Save(filePath, ImageFormat.Png);
            File.Delete(filePath);
            fd.Dispose();
        }
        [Test]
        public void SerializeToJson()
        {
            var conf = JsonConvert.SerializeObject(frameSource, Formatting.Indented, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            File.WriteAllText(ConfigPathName, conf);
        }
        [Test]
        public void DeserializeFromJson()
        {
            string fname = ConfigPathName;
            if (!File.Exists(ConfigPathName)) SerializeToJson();
            var conf = File.ReadAllText(fname);
            var fs = JsonConvert.DeserializeObject<T>(conf, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            fs.OnWorkerException += FrameSource_OnInnerException;
            fs.Freeze();
            fs.Dispose();
        }

        [Test] public void GetManagedFrame()
        {
            frameSource.Freeze();
            ManagedFrameData fd = new ManagedFrameData();
            frameSource.CopyLastFrame(fd);
        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public static string ConfigPathName
        {
            get => $"{AssemblyDirectory}/{typeof(T).ToString()}.json";
        }
    }
}
