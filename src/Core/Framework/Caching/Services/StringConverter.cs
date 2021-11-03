using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Caching.Services
{
    internal class StringConverter : IConverter<string>
    {
        public string Deserialize(string value)
        {
            return value;
        }

        public string Serialize(object obj)
        {
            return obj.ToString();
        }
    }
}
