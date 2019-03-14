using Scorpio.Auditing;
using Scorpio.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.ConsoleApplication
{
    [Authorize("Admin", "Admin1")]
    public interface ISayHelloService
    {
        [AllowAnonymous]
        [Audited]
        void SayHello();
    }

    class SayHelloService : ISayHelloService,DependencyInjection.ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
