using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DynamicPermissions.Services.HashingService
{
    public interface IHashingService
    {
        string GetSha256Hash(string input);
        int GetStableHashCode(string str);
    }
}
