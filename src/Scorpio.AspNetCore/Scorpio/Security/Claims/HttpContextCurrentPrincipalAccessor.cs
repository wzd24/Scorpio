using Microsoft.AspNetCore.Http;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Scorpio.Security.Claims
{
    internal class HttpContextCurrentPrincipalAccessor : ICurrentPrincipalAccessor, ISingletonDependency
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IPrincipal Principal => _httpContextAccessor.HttpContext?.User??Thread.CurrentPrincipal;

        public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
