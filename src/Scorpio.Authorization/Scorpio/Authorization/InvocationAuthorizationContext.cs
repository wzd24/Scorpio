using System;
namespace Scorpio.Authorization
{
    public class InvocationAuthorizationContext
    {
        public InvocationAuthorizationContext(string[] permissions)
        {
            this.Permissions = permissions;
        }

        public string[] Permissions { get;}
    }
}
