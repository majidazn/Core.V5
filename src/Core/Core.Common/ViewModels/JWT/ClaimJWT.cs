using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.JWT
{
    public class ClaimJWT 
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}