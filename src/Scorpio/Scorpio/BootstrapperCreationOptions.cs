using Microsoft.Extensions.DependencyInjection;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public class BootstrapperCreationOptions
    {
        private IServiceCollection services;

        public BootstrapperCreationOptions(IServiceCollection services)
        {
            this.services = services;
        }
    }
}