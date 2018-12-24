using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Scorpio.Data;
using Scorpio.DependencyInjection;
using Scorpio.Domain.Entities;
using Scorpio.EventBus;

namespace Scorpio.EntityFrameworkCore.EventBus
{
    class EventBusSaveChangeHandler : IOnSaveChangeHandler
    {
        private EntityChangeReport _entityChangeReport;
        private readonly IEntityChangeEventHelper _changeEventHelper;

        public EventBusSaveChangeHandler(IEntityChangeEventHelper changeEventHelper)
        {
            _changeEventHelper = changeEventHelper;
        }

        public async Task PreSaveChangeAsync(IEnumerable<EntityEntry> entries) =>
            await Task.Run(() =>
            {
                _entityChangeReport = ApplyAbpConcepts(entries);
                _changeEventHelper.TriggerChangingEventsAsync(_entityChangeReport);
            });

        public async Task PostSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            await _changeEventHelper.TriggerChangedEventsAsync(_entityChangeReport);
        }

        protected virtual EntityChangeReport ApplyAbpConcepts(IEnumerable<EntityEntry> entries)
        {
            var changeReport = new EntityChangeReport();

            foreach (var entry in entries)
            {
                ApplyAbpConcepts(entry, changeReport);
            }
            return changeReport;
        }

        protected virtual void ApplyAbpConcepts(EntityEntry entry, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyAbpConceptsForAddedEntity(entry, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyAbpConceptsForModifiedEntity(entry, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyAbpConceptsForDeletedEntity(entry, changeReport);
                    break;
            }
            AddDomainEvents(changeReport, entry.Entity);
        }

        protected virtual void ApplyAbpConceptsForAddedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
        }

        protected virtual void ApplyAbpConceptsForModifiedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            if (entry.Entity is ISoftDelete softDelete && softDelete.IsDeleted)
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
            }
            else
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Updated));
            }
        }

        protected virtual void ApplyAbpConceptsForDeletedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
        }

        protected virtual void AddDomainEvents(EntityChangeReport changeReport, object entityAsObj)
        {
            if (!(entityAsObj is IGeneratesDomainEvents generatesDomainEventsEntity))
            {
                return;
            }

            var localEvents = generatesDomainEventsEntity.DomainEvents;
            if (localEvents.Any())
            {
                changeReport.DomainEvents.AddRange(localEvents.Select(eventData => new DomainEventEntry(entityAsObj, eventData)));
                generatesDomainEventsEntity.ClearDomainEvents();
            }
        }

    }
}
