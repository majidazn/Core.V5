using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DomainDrivenDesign.Domain.SeedWork
{
    public class Base
    {
        public long UserId { get;  set; }
        public long PersonId { get;  set; }
        public DateTime CreateDate { get;  set; }
        public DateTime? ModifyDate { get;  set; }
        public string Slog { get; set; }

    }
}
