using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Scorpio.Authorization.Permissions;

namespace Scorpio.WebApplication
{
    class UserBasePermissionGrantingProvider : IPermissionGrantingProvider
    {
        public string Name =>"UserBase";

        public Task<PermissionGrantingInfo> CheckAsync(PermissionGrantingContext context)
        {
            return context.Principal?.Identity?.Name == context.Permission.Name ?
                 Task.FromResult(new PermissionGrantingInfo(true, Name)) :
                 Task.FromResult(PermissionGrantingInfo.NonGranted);
        }
    }
}
