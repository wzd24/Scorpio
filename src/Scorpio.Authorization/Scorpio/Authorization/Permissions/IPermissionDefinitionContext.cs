using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Authorization.Permissions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        PermissionGroupDefinition GetGroupOrNull(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        PermissionGroupDefinition AddGroup([NotNull] string name, string displayName = null);

    }
}
