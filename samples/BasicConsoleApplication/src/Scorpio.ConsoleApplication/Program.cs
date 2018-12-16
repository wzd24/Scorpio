using System;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper=Bootstrapper.Create<ApplicationModule>())
            {
                bootstrapper.Initialize();
                var service = bootstrapper.ServiceProvider.GetService<ISayHelloService>();
                service.SayHello();
            }
            
        }
    }
}
