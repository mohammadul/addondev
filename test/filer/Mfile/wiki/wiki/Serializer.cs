﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProtoBuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace wiki {
    public class Serializer {
        public static void Serialize<T>(string path, T data){
            using (var file = File.Create(path)) {
                ProtoBuf.Serializer.Serialize(file, data);
            } 
        }

        public static T Deserialize<T>(string path, T defaultValue) {
            
            if (!File.Exists(path)) {
                return defaultValue;
            }
            FileInfo finfo = new FileInfo(path);
            if (finfo.Length == 0) {
                return defaultValue;
            }

            T item;
            using (var file = File.OpenRead(path)) {
                item = ProtoBuf.Serializer.Deserialize<T>(file);
            }
            return item;
        }
    }

    public class JsonSerializer {
        public static string Serialize(Object obj){
            return JsonConvert.SerializeObject(obj, new JavaScriptDateTimeConverter());
        }

        public static T Deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
