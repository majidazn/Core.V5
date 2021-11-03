using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Caching.Services
{
    public interface IConverter<T>
    {
        string Serialize(object obj);

        T Deserialize(string value);
    }
}
