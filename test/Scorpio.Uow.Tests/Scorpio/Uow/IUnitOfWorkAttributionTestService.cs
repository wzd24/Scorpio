using AspectCore.DynamicProxy;

namespace Scorpio.Uow
{
    [UnitOfWork]
    public interface IUnitOfWorkAttributionTestService
    {
        void TestMothodWithUnitOfWork();

        [DisableUnitOfWork]
        void TestMothodWithoutUnitOfWork();

        IActiveUnitOfWork Current { [DisableUnitOfWork] get; }
    }

    internal class UnitOfWorkAttributionTestService : IUnitOfWorkAttributionTestService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkAttributionTestService(IUnitOfWorkManager unitOfWorkManager)
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