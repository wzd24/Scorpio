using AspectCore.DynamicProxy;

namespace Scorpio.Uow
{
    public interface IUnitOfWorkTestService
    {
        void TestMothodWithUnitOfWork();

        [DisableUnitOfWork]
        void TestMothodWithoutUnitOfWork();

        IActiveUnitOfWork Current { [DisableUnitOfWork] get; }
    }

    internal class UnitOfWorkTestService : IUnitOfWorkTestService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkTestService(IUnitOfWorkManager unitOfWorkManager)
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