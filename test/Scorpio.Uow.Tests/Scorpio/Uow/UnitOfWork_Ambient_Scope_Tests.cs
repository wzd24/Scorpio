using Scorpio.TestBase;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using Shouldly;

namespace Scorpio.Uow
{
    public class UnitOfWork_Ambient_Scope_Tests : IntegratedTest<UnitOfWorkModule>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWork_Ambient_Scope_Tests()
        {
            _unitOfWorkManager = ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task UnitOfWorkManager_Current_Should_Set_Correctly()
        {
            _unitOfWorkManager.Current.ShouldBeNull();

            using (var uow1 = _unitOfWorkManager.Begin())
            {
                _unitOfWorkManager.Current.ShouldNotBeNull();
                _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().ShouldBe(uow1);

                using (var uow2 = _unitOfWorkManager.Begin())
                {
                    _unitOfWorkManager.Current.ShouldNotBeNull();
                    _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().ShouldBe(uow1);
                    uow2.ShouldBeOfType<InnerUnitOfWorkCompleteHandle>().ShouldNotBeNull();
                    await uow2.CompleteAsync();
                }

                _unitOfWorkManager.Current.ShouldNotBeNull();
                _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().ShouldBe(uow1);

                await uow1.CompleteAsync();
            }

            _unitOfWorkManager.Current.ShouldBeNull();
        }

    }
}
