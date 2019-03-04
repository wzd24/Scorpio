using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditingHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        bool ShouldSaveAudit(MethodInfo method);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        AuditActionInfo CreateAuditAction();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="implementationMethod"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        AuditActionInfo CreateAuditAction(Type type, MethodInfo implementationMethod, object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="implementationMethod"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        AuditActionInfo CreateAuditAction(Type type, MethodInfo implementationMethod, IDictionary<string, object> parameters);

    }
}