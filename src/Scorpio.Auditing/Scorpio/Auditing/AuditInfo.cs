using System;
using System.Collections.Generic;
using System.Text;
using Scorpio;
namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string CurrentUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImpersonatorUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan ExecutionDuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<AuditActionInfo> Actions { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<Exception> Exceptions { get; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, object> ExtraProperties { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> Comments { get;  }

        /// <summary>
        /// 
        /// </summary>
        public AuditInfo()
        {
            ExtraProperties = new Dictionary<string, object>();
            Actions = new List<AuditActionInfo>();
            Exceptions = new List<Exception>();
            Comments = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetExtraProperty<T>(string name)
        {
            return (T)(ExtraProperties.GetOrDefault(name) ?? default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void SetExtraProperty(string name,object value)
        {
            ExtraProperties[name] = value;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AuditActionInfo
    {

        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan ExecutionDuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; }

        /// <summary>
        /// 
        /// </summary>
        public AuditActionInfo()
        {
            ExtraProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetExtraProperty<T>(string name)
        {
            return (T)(ExtraProperties.GetOrDefault(name) ?? default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        protected void SetExtraProperty(string name, object value)
        {
            ExtraProperties[name] = value;
        }

    }
}
