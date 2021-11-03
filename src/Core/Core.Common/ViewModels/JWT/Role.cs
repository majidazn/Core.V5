using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Common.ViewModels.JWT
{
    public class Role
    { 

        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public object Users { get; set; }
        public ClaimJWT[] Claims { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }



    }
}
