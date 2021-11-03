using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.DynamicPermissions
{
    public class MvcActionViewModel
    {
        public IList<Attribute> ActionAttributes { get; set; }
        public string ActionDisplayName { get; set; }
        public string ActionId => $"{ControllerId}:{ActionName}";
        public string ActionName { get; set; }
        public string ControllerId { get; set; }
        public bool IsSecuredAction { get; set; }
        public string ActionIdNonSecure { get; set; }

        public override string ToString()
        {
            const string attribute = "Attribute";
            var actionAttributes = string.Join(",", ActionAttributes.Select(a => a.GetType().Name.Replace(attribute, "")));
            return $"[{actionAttributes}]{ActionName}";
        }
    }
}
