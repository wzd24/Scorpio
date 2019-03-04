using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scorpio.HostService
{
    class HostedService : Microsoft.Extensions.Hosting.BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => Console.WriteLine("Background service stopping now!"));
            Console.WriteLine("Background service startting now!");
            for (var i = 1; i>0; i++)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                await Task.Delay(1000, stoppingToken);
                Console.WriteLine($"Background service has been invoked {i} time(s).");
            }
        }

    }
}
