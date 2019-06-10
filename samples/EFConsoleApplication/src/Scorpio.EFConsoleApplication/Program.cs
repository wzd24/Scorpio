using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scorpio.Data;
using Microsoft.Extensions.Configuration;
using Scorpio.Application.Dtos;

namespace Scorpio.EFConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<ApplicationModule>(opt=>
            {
                opt.Configuration(cb =>
                {
                    cb.AddJsonFile("appsettings.json");
                });
            }))
            {
                bootstrapper.Initialize();
                var service = bootstrapper.ServiceProvider.GetRequiredService<IUserService>();
                //service.Create(new UserDto
                //{
                //    Name = "宋八",
                //    Age = 34,
                //});

                service.Delete(u=>u.Id==5);
                var request = new ListRequest<UserDto>().Sort("ID desc").Where("ID<@0",7);
                using (bootstrapper.ServiceProvider.GetRequiredService<IDataFilter<ISoftDelete>>().Disable())
                {
                    foreach (var item in service.GetList(request))
                    {
                        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
                    }
                }
            }
        }
    }
}
