// Copyright (c) 2020 Aleksei Osipov [Lupas] https://github.com/LupasTools
// JsonFile.cs : 30.8.2020
// MIT license

using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Lupas.Samples.FrameSource
{
    public static class JsonFile
    {
        public static void Serialize(object obj)
        {
            var conf = JsonConvert.SerializeObject(obj, Formatting.Indented, JsonConfig);
            File.WriteAllText(ConfigPathName(obj.GetType()), conf);
        }
        public static T Deserialize<T>() where T: new()
        {
            string fname = ConfigPathName(typeof(T));
            if (!File.Exists(fname)) return new T();
            var conf = File.ReadAllText(fname);
            return (T)JsonConvert.DeserializeObject<T>(conf, JsonConfig);
        }

        private static JsonSerializerSettings JsonConfig => 
            new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        private static string ConfigPathName(Type type)
        {
            return $"{AssemblyDirectory}/{type.ToString()}.json";
        }
    }
}
