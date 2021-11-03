using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Attributes
{
    public class CustomAuthorizeAttribute: Attribute
    {
        
        public CustomAuthorizeAttribute()
        { }
        public CustomAuthorizeAttribute(string policy)
        {
            this.Policy = policy;
        }

        
        public string Policy { get; set; }
        
        public string Roles { get; set; }
        
        public string AuthenticationSchemes { get; set; }
        
        [Obsolete("Use AuthenticationSchemes instead.", false)]
        public string ActiveAuthenticationSchemes { get; set; }
    }
}
