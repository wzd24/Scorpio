using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Authorization.Permissions;

namespace Scorpio.ConsoleApplication
{
    class PermissionProvider : IPermissionDefinitionProvider
    {
        public void Define(IPermissionDefinitionContext context)
        {
            context.AddGroup("Default").AddPermission("Admin").AddPermission("Admin1");
        }
    }
}
