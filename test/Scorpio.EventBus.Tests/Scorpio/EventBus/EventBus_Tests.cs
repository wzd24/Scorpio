using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Moq;

namespace Scorpio.EventBus
{
    public class EventBus_Tests : TestBase.IntegratedTest<EventBusModule>
    {
        private IEventBus _eventBus;

        public EventBus_Tests()
        {
            _eventBus = ServiceProvider.GetService<IEventBus>();
        }

        [Fact]
        public void RegisterAction()
        {
            _eventBus.Register<string>(s => Task.Run(() => Console.WriteLine(s)));
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance
                .ShouldBeOfType<ActionEventHandler<string>>();
        }

        [Fact]
        public void RegisterEventHandler()
        {
            var mock = new Mock<IEventHandler<string>>();
            _eventBus.Register(mock.Object);
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance.ShouldBe(mock.Object);
        }

        [Fact]
        public void RegisterGenericEventHandler()
        {
            _eventBus.Register<string, TestClasses.EmptyEventHandler>();
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<TransientEventHandlerFactory<TestClasses.EmptyEventHandler>>().GetHandler().EventHandler
                .ShouldBeOfType<TestClasses.EmptyEventHandler>();
        }

        [Fact]
        public void RegisterEventHandlerFactory()
        {
            var mock = new Mock<IEventHandlerFactory>();
            _eventBus.Register<string>(mock.Object);
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem().ShouldBe(mock.Object);

        }

        [Fact]
        public void UnRegisterAction()
        {
            Func<string, Task> action = s => Task.Run(() => Console.WriteLine(s));
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.Clear();
            _eventBus.Register(action);
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance
                .ShouldBeOfType<ActionEventHandler<string>>();
            _eventBus.Unregister(action);
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldBeEmpty();
        }

        [Fact]
        public void UnRegisterEventHandler()
        {
            var mock = new Mock<IEventHandler<string>>();
            _eventBus.Register(mock.Object);
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance.ShouldBe(mock.Object);
            _eventBus.Unregister(mock.Object);
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldBeEmpty();
        }

        [Fact]
        public void UnRegisterEventHandlerFactory()
        {
            var mock = new Mock<IEventHandlerFactory>();
            _eventBus.Register<string>(mock.Object);
            _eventBus.ShouldBeOfType<EventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem().ShouldBe(mock.Object);
            _eventBus.Unregister<string>(mock.Object);
            _eventBus.ShouldBeOfType<EventBus>()
                .HandlerFactories[typeof(string)].ShouldBeEmpty();

        }
    }
}
