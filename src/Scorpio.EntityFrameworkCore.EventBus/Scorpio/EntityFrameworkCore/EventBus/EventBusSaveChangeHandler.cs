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
                _entityChangeReport = ApplyConcepts(entries);
                _changeEventHelper.TriggerChangingEventsAsync(_entityChangeReport);
            });

        public async Task PostSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            await _changeEventHelper.TriggerChangedEventsAsync(_entityChangeReport);
        }

        protected virtual EntityChangeReport ApplyConcepts(IEnumerable<EntityEntry> entries)
        {
            var changeReport = new EntityChangeReport();

            foreach (var entry in entries)
            {
                ApplyConcepts(entry, changeReport);
            }
            return changeReport;
        }

        protected virtual void ApplyConcepts(EntityEntry entry, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyConceptsForModifiedEntity(entry, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyConceptsForDeletedEntity(entry, changeReport);
                    break;
            }
            AddDomainEvents(changeReport, entry.Entity);
        }

        protected virtual void ApplyConceptsForAddedEntity(EntityEntry entry, EntityChangeReport changeReport)
        {
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
        }

        protected virtual void ApplyConceptsForModifiedEntity(EntityEntry entry, EntityChangeReport changeReport)
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

        protected virtual void ApplyConceptsForDeletedEntity(EntityEntry entry, EntityChangeReport changeReport)
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
