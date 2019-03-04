using Scorpio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore.EventBus
{
    /// <summary>
    /// Used to pass data for an event that is related to with an <see cref="IEntity"/> object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityEventData<TEntity> : Scorpio.EventBus.IEventDataWithInheritableGenericArgument
    {

        /// <summary>
        /// Related entity with this event.
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">Related entity with this event</param>
        public EntityEventData(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual object[] GetConstructorArgs()
        {
            return new object[] { Entity };
        }
    }

    /// <summary>
    /// Used to pass data for an event when an entity (<see cref="IEntity"/>) is changed (created, updated or deleted).
    /// See <see cref="EntityCreatedEventData{TEntity}"/>, <see cref="EntityDeletedEventData{TEntity}"/> and <see cref="EntityUpdatedEventData{TEntity}"/> classes.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">Changed entity in this event</param>
        public EntityChangedEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// Used to pass data for an event when an entity (<see cref="IEntity"/>) is changed (created, updated or deleted).
    /// See <see cref="EntityCreatingEventData{TEntity}"/>, <see cref="EntityDeletingEventData{TEntity}"/> and <see cref="EntityUpdatingEventData{TEntity}"/> classes.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityChangingEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">Changed entity in this event</param>
        public EntityChangingEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// This type of event can be used to notify just after creation of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityCreatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is created</param>
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// This type of event can be used to notify just after creation of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is created</param>
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// This type of event can be used to notify just after deletion of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is deleted</param>
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// This type of event can be used to notify just after deletion of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityDeletingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is deleted</param>
        public EntityDeletingEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// This type of event can be used to notify just after update of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityUpdatedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is updated</param>
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {

        }
    }

    /// <summary>
    /// This type of event can be used to notify just after update of an Entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityUpdatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">The entity which is updated</param>
        public EntityUpdatingEventData(TEntity entity)
            : base(entity)
        {

        }
    }
}
