using Microsoft.Extensions.DependencyInjection;
using Scorpio.TestBase;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Scorpio.Uow
{
    public class UnitOfWork_Attribution_Tests : IntegratedTest<UnitOfWork_Attribution_Module>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWork_Attribution_Tests()
        {
            _unitOfWorkManager = ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public void Should_Create_Nested_UnitOfWorks()
        {
            _unitOfWorkManager.Current.ShouldBeNull();
            var service = ServiceProvider.GetRequiredService<IUnitOfWorkAttributionTestService>();
            service.ShouldNotBeNull();
            service.Current.ShouldBeNull();
            service.TestMothodWithUnitOfWork();
            service.Current.ShouldNotBeNull();
            _unitOfWorkManager.Current.ShouldBeNull();
            service.TestMothodWithoutUnitOfWork();
            service.Current.ShouldBeNull();
            _unitOfWorkManager.Current.ShouldBeNull();
        }

    }
}
