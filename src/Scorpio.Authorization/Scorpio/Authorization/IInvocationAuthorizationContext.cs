using System;
namespace Scorpio.Authorization
{
    public interface IInvocationAuthorizationContext
    {
        string[] Permissions { get; }
    }
}
