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
    public class UnitOfWork_Nested_Tests : IntegratedTest<UnitOfWorkModule>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWork_Nested_Tests()
        {
            _unitOfWorkManager = ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        }

        [Fact]
        public async Task Should_Create_Nested_UnitOfWorks()
        {
            _unitOfWorkManager.Current.ShouldBeNull();

            using (var uow1 = _unitOfWorkManager.Begin())
            {
                _unitOfWorkManager.Current.ShouldNotBeNull();
                _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().ShouldBe(uow1);

                using (var uow2 = _unitOfWorkManager.Begin(System.Transactions.TransactionScopeOption.RequiresNew))
                {
                    _unitOfWorkManager.Current.ShouldNotBeNull();
                    _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().ShouldNotBe(uow1);

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
