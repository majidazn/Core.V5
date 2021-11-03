using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.ViewModels.JWT
{
    public class ValidateToken
    {
        public int UserId { get; set; }
        public string TokenRawData { get; set; }
        public int TenantId { get; set; }
    }
}
