using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeAttribute : AbstractInterceptorAttribute
    {

        /// <summary>
        /// 
        /// </summary>
        public AuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// 
        /// </summary>
        public string[] Permissions { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
