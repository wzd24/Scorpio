using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Data
{
    public class DataFilterDescriptor_Tests
    {
        [Fact]
        public void CtorEnable()
        {
            var descriptor = new SoftDeleteDataFilterDescriptor { IsEnabled = true };
            descriptor.FilterType.ShouldBe(typeof(ISoftDelete));
            descriptor.IsEnabled.ShouldBeTrue();
            descriptor.GetState().IsEnabled.ShouldBeTrue();
            var mock = new Moq.Mock<IServiceProvider>();
            var options = new DataFilterOptions();
            options.RegiesterFilter(descriptor);
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ISoftDelete>(new OptionsWrapper<DataFilterOptions>(options)));
            var filter = new DataFilter(mock.Object);
            var filterContext = new FakeFilterContext();
            descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
            descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            using (filter.Disable<ISoftDelete>())
            {
                descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
                descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            }
        }

        [Fact]
        public void CtorDisable()
        {
            var descriptor = new SoftDeleteDataFilterDescriptor { IsEnabled = false };
            descriptor.FilterType.ShouldBe(typeof(ISoftDelete));
            descriptor.IsEnabled.ShouldBeFalse();
            descriptor.GetState().IsEnabled.ShouldBeFalse();
            var mock = new Moq.Mock<IServiceProvider>();
            var options = new DataFilterOptions();
            options.RegiesterFilter(descriptor);
            mock.Setup(s => s.GetService(Moq.It.IsAny<Type>())).Returns(new DataFilter<ISoftDelete>(new OptionsWrapper<DataFilterOptions>(options)));
            var filter = new DataFilter(mock.Object);
            var filterContext = new FakeFilterContext();
            descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeTrue();
            descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            using (filter.Enable<ISoftDelete>())
            {
                descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(true)).ShouldBeFalse();
                descriptor.BuildFilterExpression<SoftDeleteEntity>( filter, filterContext).Compile()(new SoftDeleteEntity(false)).ShouldBeTrue();
            }
        }

    }
}
