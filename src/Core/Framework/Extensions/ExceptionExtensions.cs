using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                 ? ex.Message
                 : ex.Message + " --> " + ex.InnerException.GetFullMessage();
        }
    }
}
