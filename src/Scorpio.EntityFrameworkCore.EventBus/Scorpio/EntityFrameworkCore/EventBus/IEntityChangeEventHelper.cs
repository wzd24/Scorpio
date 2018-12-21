using Scorpio.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.EntityFrameworkCore.EventBus
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
        Task TriggerEventsAsync(EntityChangeReport changeReport);

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
        Task TriggerEntityCreatedEventOnUowCompletedAsync(object entity);

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
        Task TriggerEntityUpdatedEventOnUowCompletedAsync(object entity);

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
        Task TriggerEntityDeletedEventOnUowCompletedAsync(object entity);
    }
}
