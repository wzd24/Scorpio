using System;
using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    public interface IAuthorizationService
    {
        Task CheckAsync(IInvocationAuthorizationContext authorizationContext);
    }
}
