using System;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<ApplicationModule>())
            {
                bootstrapper.Initialize();
                System.Threading.Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Admin1"), new string[] { });
                var service = bootstrapper.ServiceProvider.GetService<ISayHelloService>();
                var auditingManager = bootstrapper.ServiceProvider.GetService<Auditing.IAuditingManager>();
                using (auditingManager.BeginScope())
                {
                    service.SayHello();
                }
            }

        }
    }
}
