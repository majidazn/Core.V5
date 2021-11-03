using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utilities
{
    public class ParenthesesMatcher
    {

        public static bool IsMatched(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            Stack<char> stack = new Stack<char>();

            foreach (var s in str)
            {
                if (s == '(')
                    stack.Push(s);

                if (s == ')')
                    if (!stack.TryPop(out char tempChar))
                        return false;
            }

            if (stack.Count == 0)
                return true;

            return false;
        }
    }
}
