using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Scorpio.HostService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.AddBootstrapper<ApplicationModule>();
            await hostBuilder.RunConsoleAsync();
        }
    }
}
