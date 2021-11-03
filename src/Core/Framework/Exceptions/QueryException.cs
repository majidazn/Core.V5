using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace Framework.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException(HttpStatusCode code = HttpStatusCode.BadRequest
                    , object errors = null)
            :base()
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; }
        public object Errors { get; }
    }
}
