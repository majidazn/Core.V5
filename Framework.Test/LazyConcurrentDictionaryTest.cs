using System;
using System.Collections.Generic;
using Xunit;
using Framework.Extensions;
using Framework.Concurrents;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Framework.Test
{
    public class LazyConcurrentDictionaryTest
    {


        [Fact]
        public void GetOrAdd_IsNotSafe_Test()
        {
            var dictionary = new ConcurrentDictionary<int, int>();
            var options = new ParallelOptions { MaxDegreeOfParallelism = 100 };
            var addStack = new ConcurrentStack<int>();

            Parallel.For(1, 1000, options, i =>
            {
                var key = i % 10;
                dictionary.GetOrAdd(key, k =>
                {
                    addStack.Push(k);
                    return i;
                });
            });


            Xunit.Assert.Equal(dictionary.Count, addStack.Count);

            Console.WriteLine($"dictionary.Count: {dictionary.Count}");
            Console.WriteLine($"addStack.Count: {addStack.Count}");
        }




        [Fact]
        public void GetOrAddTest()
        {
            LazyConcurrentDictionary<int, List<int>> lazyConcurrentDictionary = new LazyConcurrentDictionary<int, List<int>>();
            var ints = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var options = new ParallelOptions { MaxDegreeOfParallelism = 100 };
            var addStack = new ConcurrentStack<List<int>>();

            Parallel.For(1, 1000, options, i =>
            {
                var key = i % 10;

                //lazyConcurrentDictionary.AddOrUpdate(i, ints, (k, oldValue) =>
                //{
                //    addStack.Push(ints);
                //    return ints;
                //});

                lazyConcurrentDictionary.GetOrAdd(i, (k) =>
                {
                    addStack.Push(ints);
                    return new List<int>() { i };
                });


            });

            Xunit.Assert.Equal(addStack.Count, lazyConcurrentDictionary.Count);

        }
    }
}
