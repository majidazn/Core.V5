using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.DynamicPermissions.Services.MvcActionsDiscovery
{
    public interface IMvcActionsDiscoveryService
    {
        ICollection<MvcControllerViewModel> MvcControllers { get; }
        ICollection<MvcControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }
}
