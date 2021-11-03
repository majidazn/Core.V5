using Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.Test
{
    public class ParenthesesMatcherTest
    {
            [Fact]
            public void Test1()
            {
                var str = "( () ( () ) () ( ) )";
                var response = ParenthesesMatcher.IsMatched(str);
                Assert.True(response);
            }

            [Fact]
            public void Test2()
            {
                var str = "( () ( () ) () ( ) )   )";
                var response = ParenthesesMatcher.IsMatched(str);
                Assert.False(response);
            }

            [Fact]
            public void Test3()
            {
                var str = "( () ( () ) () ( ) )  ( )";
                var response = ParenthesesMatcher.IsMatched(str);
                Assert.True(response);
            }

            [Fact]
            public void Test4()
            {
                var str = "( () ( () ) () ( ) ( ) )  )";
                var response = ParenthesesMatcher.IsMatched(str);
                Assert.False(response);
            }

            [Fact]
            public void Test5()
            {
                var str = ") ( () ( () ) () ( ) ( ) )  )";
                var response = ParenthesesMatcher.IsMatched(str);
                Assert.False(response);
            }
    }   
}
