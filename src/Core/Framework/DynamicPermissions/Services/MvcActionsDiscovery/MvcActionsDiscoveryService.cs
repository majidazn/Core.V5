﻿using Framework.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Framework.DynamicPermissions.Services.MvcActionsDiscovery
{
    public class MvcActionsDiscoveryService : IMvcActionsDiscoveryService
    {
        // 'GetOrAdd' call on the dictionary is not thread safe and we might end up creating the GetterInfo more than
        // once. To prevent this Lazy<> is used. In the worst case multiple Lazy<> objects are created for multiple
        // threads but only one of the objects succeeds in creating a GetterInfo.
        private readonly ConcurrentDictionary<string, Lazy<ICollection<MvcControllerViewModel>>> _allSecuredActionsWithPloicy =
            new ConcurrentDictionary<string, Lazy<ICollection<MvcControllerViewModel>>>();

        public MvcActionsDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            if (actionDescriptorCollectionProvider == null)
                throw new Exception($@"{nameof(actionDescriptorCollectionProvider)} is null");
            //actionDescriptorCollectionProvider.CheckArgumentIsNull(nameof(actionDescriptorCollectionProvider));

            MvcControllers = new List<MvcControllerViewModel>();

            var lastControllerName = string.Empty;
            MvcControllerViewModel currentController = null;

            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                var descriptor = actionDescriptor as ControllerActionDescriptor;
                if (descriptor == null)
                {
                    continue;
                }

                var controllerTypeInfo = descriptor.ControllerTypeInfo;
                var actionMethodInfo = descriptor.MethodInfo;

                if (lastControllerName != descriptor.ControllerName)
                {
                    currentController = new MvcControllerViewModel
                    {
                        AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                        ControllerAttributes = getAttributes(controllerTypeInfo),
                        ControllerDisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                        ControllerName = descriptor.ControllerName,
                    };
                    MvcControllers.Add(currentController);

                    lastControllerName = descriptor.ControllerName;
                }

                currentController?.MvcActions.Add(new MvcActionViewModel
                {
                    ControllerId = currentController.ControllerId,
                    ActionName = descriptor.ActionName,
                    ActionDisplayName = actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    ActionAttributes = getAttributes(actionMethodInfo),
                    IsSecuredAction = isSecuredAction(controllerTypeInfo, actionMethodInfo)
                });
            }
        }

        public ICollection<MvcControllerViewModel> MvcControllers { get; }

        public ICollection<MvcControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName)
        {
            var getter = _allSecuredActionsWithPloicy.GetOrAdd(policyName, y => new Lazy<ICollection<MvcControllerViewModel>>(
                () =>
                {
                    var controllers = new List<MvcControllerViewModel>(MvcControllers);
                    foreach (var controller in controllers)
                    {
                        controller.MvcActions = controller.MvcActions.Where(
                            model => model.IsSecuredAction &&
                            (
                            model.ActionAttributes.OfType<CustomAuthorizeAttribute>().FirstOrDefault()?.Policy == policyName ||
                            controller.ControllerAttributes.OfType<CustomAuthorizeAttribute>().FirstOrDefault()?.Policy == policyName
                            )).ToList();
                    }
                    return controllers.Where(model => model.MvcActions.Any()).ToList();
                }));
            return getter.Value;
        }

        private static List<Attribute> getAttributes(MemberInfo actionMethodInfo)
        {
            return actionMethodInfo.GetCustomAttributes(inherit: true)
                                   .Where(attribute =>
                                   {
                                       var attributeNamespace = attribute.GetType().Namespace;
                                       return attributeNamespace != typeof(CompilerGeneratedAttribute).Namespace &&
                                              attributeNamespace != typeof(DebuggerStepThroughAttribute).Namespace;
                                   })
                                    .Cast<Attribute>()
                                   .ToList();
        }

        private static bool isSecuredAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            var actionHasAllowAnonymousAttribute = actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(inherit: true) != null;
            if (actionHasAllowAnonymousAttribute)
            {
                return false;
            }

            var controllerHasAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<CustomAuthorizeAttribute>(inherit: true) != null;
            if (controllerHasAuthorizeAttribute)
            {
                return true;
            }

            var actionMethodHasAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<CustomAuthorizeAttribute>(inherit: true) != null;
            if (actionMethodHasAuthorizeAttribute)
            {
                return true;
            }

            return false;
        }
    }
}
