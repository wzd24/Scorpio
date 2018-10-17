using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    class InternalBootstrapper : Bootstrapper
    {
        private bool _isShutdown = false;
        public InternalBootstrapper(Type startupModuleType, IServiceCollection services, Action<BootstrapperCreationOptions> optionsAction) : base(startupModuleType, services, optionsAction)
        {
            
        }

        public override void Shutdown()
        {
            if (_isShutdown)
            {
                return;
            }
            _isShutdown = true;
            base.Shutdown();
        }

        public override void Dispose()
        {
            Shutdown();
            base.Dispose();
        }
    }
}
