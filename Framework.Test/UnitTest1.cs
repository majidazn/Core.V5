using System;
using System.Collections.Generic;
using Xunit;
using Framework.Extensions;

namespace Framework.Test
{
    public class StableHashCodeTest
    {
        [Fact]
        public void Test1()
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            Dictionary<int, string> keyValuePairs2 = new Dictionary<int, string>();

            for (int i = 0; i < 1_00_000; i++)
            {
                keyValuePairs.Add($@"this is a str {i}".GetStableHashCode(), "");
            }

            for (int i = 0; i < 1_00_000; i++)
            {
                keyValuePairs2.Add($@"this is a str {i}".GetStableHashCode(), "");
            }


            foreach (var item in keyValuePairs)
            {
                if (!keyValuePairs2.ContainsKey(item.Key))
                    throw new Exception("key is not there");
            }


        }
    }
}
