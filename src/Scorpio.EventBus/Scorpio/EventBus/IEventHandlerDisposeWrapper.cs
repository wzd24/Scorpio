using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandlerDisposeWrapper : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        IEventHandler EventHandler { get; }
    }
}
