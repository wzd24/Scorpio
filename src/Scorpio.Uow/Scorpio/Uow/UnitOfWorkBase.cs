using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private bool _isBeginCalledBefore;
        private bool _isCompleteCalledBefore;
        private readonly UnitOfWorkDefaultOptions _defaultOptions;

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWorkOptions Options { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 
        /// </summary>
        protected UnitOfWorkBase(
            IServiceProvider serviceProvider,
             IOptions<UnitOfWorkDefaultOptions> options
            )
        {
            _defaultOptions = options.Value;
            ServiceProvider = serviceProvider;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Begin(UnitOfWorkOptions options)
        {
            Check.NotNull(options, nameof(options));

            PreventMultipleBegin();
            Options = _defaultOptions.Normalize(options.Clone()); //TODO: Do not set options like that, instead make a copy?
            BeginUow();
        }


        public void Complete()
        {
            throw new NotImplementedException();
        }

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Can be implemented by derived classes to start UOW.
        /// </summary>
        protected virtual void BeginUow()
        {

        }

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();

        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new ScorpioException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new ScorpioException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }
    }
}
