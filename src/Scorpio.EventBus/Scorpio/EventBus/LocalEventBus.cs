﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspectCore.Injector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Scorpio.DependencyInjection;
using Scorpio.Threading;

namespace Scorpio.EventBus
{
    internal class LocalEventBus : EventBusBase
    {

        /// <summary>
        /// Reference to the Logger.
        /// </summary>
        [FromContainer]
        public ILogger<LocalEventBus> Logger { get; set; }


        public LocalEventBus(IOptions<EventBusOptions> options, IHybridServiceScopeFactory serviceScopeFactory) : base(options, serviceScopeFactory)
        {
            Logger = NullLogger<LocalEventBus>.Instance;
        }


        public override async Task PublishAsync(Type eventType, object eventData)
        {
            var exceptions = new List<Exception>();

            await TriggerHandlersAsync(eventType, eventData, exceptions);

            if (exceptions.Any())
            {
                if (exceptions.Count == 1)
                {
                    exceptions[0].ReThrow();
                }

                throw new AggregateException("More than one error has occurred while triggering the event: " + eventType, exceptions);
            }
        }

        public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType)
                           .Locking(factories => factories.Add(factory));

            return new EventHandlerFactoryUnregistrar(this, eventType, factory);
        }


        public override void Unsubscribe<TEvent>(Func<TEvent, Task> action)
        {
            GetOrCreateHandlerFactories(typeof(TEvent))
                          .Locking(factories =>
                          {
                              factories.RemoveAll(
                                  factory =>
                                  {
                                      if (!(factory is SingleInstanceHandlerFactory singleInstanceFactory))
                                      {
                                          return false;
                                      }

                                      if (!(singleInstanceFactory.HandlerInstance is ActionEventHandler<TEvent> actionHandler))
                                      {
                                          return false;
                                      }

                                      return actionHandler.Action == action;
                                  });
                          });
        }

        public override void Unsubscribe(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType)
                .Locking(factories =>
                {
                    factories.RemoveAll(
                        factory =>
                            factory is SingleInstanceHandlerFactory &&
                            (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                    );
                });
        }

        public override void Unsubscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
        }

        public override void UnsubscribeAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
        }

        protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

            foreach (var handlerFactory in HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
            {
                handlerFactoryList.Add(new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
            }

            return handlerFactoryList.ToArray();
        }

        private bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            if (handlerType == eventType)
            {
                return true;
            }

            //Should trigger for inherited types
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            return HandlerFactories.GetOrAdd(eventType, (type) => new List<IEventHandlerFactory>());
        }

    }
}
