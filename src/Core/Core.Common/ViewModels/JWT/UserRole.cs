using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.JWT
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsUserInTheRole { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
