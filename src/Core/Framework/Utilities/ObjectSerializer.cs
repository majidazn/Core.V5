using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utilities
{
    public static class ObjectSerializer<T> 
    {
        public static string SerializingObject(T obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.None;
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }

        public static T DeserilizngObject(string value)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.None;

            return JsonConvert.DeserializeObject<T>(value, jsonSerializerSettings);
        }
    }
}
