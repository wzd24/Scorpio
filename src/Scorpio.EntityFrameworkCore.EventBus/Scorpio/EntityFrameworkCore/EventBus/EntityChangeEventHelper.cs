using Scorpio.DependencyInjection;
using Scorpio.DynamicProxy;
using Scorpio.EventBus;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.EntityFrameworkCore.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    class EntityChangeEventHelper : IEntityChangeEventHelper,ITransientDependency
    {
        private readonly IEventBus _eventBus;
        protected IUnitOfWorkManager UnitOfWorkManager { get; }

        public EntityChangeEventHelper(IEventBus eventBus, IUnitOfWorkManager unitOfWorkManager)
        {
            _eventBus = eventBus;
            UnitOfWorkManager = unitOfWorkManager;
        }


        public async Task TriggerChangedEventsAsync(EntityChangeReport changeReport)
        {
            foreach (var changedEntity in changeReport.ChangedEntities)
            {
                switch (changedEntity.ChangeType)
                {
                    case EntityChangeType.Created:
                        await TriggerEntityCreatedEventAsync(changedEntity.Entity);
                        break;
                    case EntityChangeType.Updated:
                        await TriggerEntityUpdatedEventAsync(changedEntity.Entity);
                        break;
                    case EntityChangeType.Deleted:
                        await TriggerEntityDeletedEventAsync(changedEntity.Entity);
                        break;
                    default:
                        throw new ScorpioException("Unknown EntityChangeType: " + changedEntity.ChangeType);
                }
            }
            foreach (var domainEvent in changeReport.DomainEvents)
            {
                await _eventBus.PublishAsync(domainEvent.EventData.GetType(), domainEvent.EventData);
            }
        }

        public async Task TriggerChangingEventsAsync(EntityChangeReport changeReport)
        {
            foreach (var changedEntity in changeReport.ChangedEntities)
            {
                switch (changedEntity.ChangeType)
                {
                    case EntityChangeType.Created:
                        await TriggerEntityCreatingEventAsync(changedEntity.Entity);
                        break;
                    case EntityChangeType.Updated:
                        await TriggerEntityUpdatingEventAsync(changedEntity.Entity);
                        break;
                    case EntityChangeType.Deleted:
                        await TriggerEntityDeletingEventAsync(changedEntity.Entity);
                        break;
                    default:
                        throw new ScorpioException("Unknown EntityChangeType: " + changedEntity.ChangeType);
                }
            }
        }

        public async Task TriggerEntityCreatedEventAsync(object entity)
        {
            await TriggerEventWithEntity(_eventBus, typeof(EntityCreatedEventData<>), entity, false);
        }

        public async Task TriggerEntityCreatingEventAsync(object entity)
        {
            await TriggerEventWithEntity(_eventBus, typeof(EntityCreatingEventData<>), entity, true);
        }

        public async Task TriggerEntityDeletedEventAsync(object entity)
        {
            await TriggerEventWithEntity(_eventBus, typeof(EntityDeletedEventData<>), entity, false);
        }

        public async Task TriggerEntityDeletingEventAsync(object entity)
        {
            await TriggerEventWithEntity(_eventBus, typeof(EntityDeletingEventData<>), entity, true);
        }

        public async Task TriggerEntityUpdatedEventAsync(object entity)
        {
            await TriggerEventWithEntity(_eventBus, typeof(EntityUpdatedEventData<>), entity, false);
        }

        public async Task TriggerEntityUpdatingEventAsync(object entity)
        {
            await TriggerEventWithEntity(_eventBus, typeof(EntityUpdatingEventData<>), entity, true);
        }

        protected virtual async Task TriggerEventWithEntity(IEventBus eventPublisher, Type genericEventType, object entity, bool triggerInCurrentUnitOfWork)
        {
            var entityType = ProxyHelper.UnProxy(entity).GetType();
            var eventType = genericEventType.MakeGenericType(entityType);

            if (triggerInCurrentUnitOfWork || UnitOfWorkManager.Current == null)
            {
                await eventPublisher.PublishAsync(eventType, Activator.CreateInstance(eventType, entity));
                return;
            }
            UnitOfWorkManager.Current.Completed += ((o, e) => eventPublisher.PublishAsync(eventType, Activator.CreateInstance(eventType, entity)));
        }

    }
}
