using AspectCore.DynamicProxy;

namespace Scorpio.Uow
{
    public interface IUnitOfWorkConventionalTestService
    {
        void TestMothodWithUnitOfWork();

        [DisableUnitOfWork]
        void TestMothodWithoutUnitOfWork();

        IActiveUnitOfWork Current { [DisableUnitOfWork] get; }
    }

    internal class UnitOfWorkConventionalTestService : IUnitOfWorkConventionalTestService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkConventionalTestService(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public IActiveUnitOfWork Current { get; set; }
        public void TestMothodWithoutUnitOfWork()
        {
            Current= _unitOfWorkManager.Current;
        }

        public void TestMothodWithUnitOfWork()
        {
            Current = _unitOfWorkManager.Current;
        }
    }
}