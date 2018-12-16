using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.ConsoleApplication
{
   public interface ISayHelloService
    {
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
