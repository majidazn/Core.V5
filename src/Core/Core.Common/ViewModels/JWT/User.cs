﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.JWT
{
    public class User
    {
        public User()
        {
            Roles = new HashSet<UserRole>();
            UserTokens = new HashSet<UserToken>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public bool IsActive { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonId { get; set; }
        public string GreatestRoleName { get; set; }

        public DateTimeOffset? LastLoggedIn { get; set; }

        /// <summary>
        /// every time the user changes his Password,
        /// or an admin changes his Roles or stat/IsActive,
        /// create a new `SerialNumber` GUID and store it in the DB.
        /// </summary>
        public string SerialNumber { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
