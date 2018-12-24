using Scorpio.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.EventBus
{
    /// <summary>
    /// Used to trigger entity change events.
    /// </summary>
    public interface IEntityChangeEventHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="changeReport"></param>
        /// <returns></returns>
        Task TriggerChangingEventsAsync(EntityChangeReport changeReport);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changeReport"></param>
        /// <returns></returns>
        Task TriggerChangedEventsAsync(EntityChangeReport changeReport);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task TriggerEntityCreatingEventAsync(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task TriggerEntityCreatedEventAsync(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task TriggerEntityUpdatingEventAsync(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task TriggerEntityUpdatedEventAsync(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task TriggerEntityDeletingEventAsync(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task TriggerEntityDeletedEventAsync(object entity);
    }
}
