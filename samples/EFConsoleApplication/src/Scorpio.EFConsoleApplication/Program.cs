﻿using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.EFConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var bootstrapper = Bootstrapper.Create<ApplicationModule>())
            {
                bootstrapper.Initialize();
                var uowm = bootstrapper.ServiceProvider.GetService<Uow.IUnitOfWorkManager>();
                using (var uow = uowm.Begin())
                {
                    var repo = bootstrapper.ServiceProvider.GetService<Domain.Repositories.IRepository<User>>();
                    using (var uow2 = uowm.Begin(System.Transactions.TransactionScopeOption.RequiresNew))
                    {
                        repo.Insert(new User
                        {
                            Name = "李四",
                            Age = 48,
                        });
                        uow2.Complete();
                    }
                    using (var uow2 = uowm.Begin(System.Transactions.TransactionScopeOption.RequiresNew))
                    {
                        repo.Insert(new User
                        {
                            Name = "王五",
                            Age = 24,
                        });
                    }
                    repo.Insert(new User
                    {
                        Name = "张三",
                        Age = 22,
                    });
                    uow.Complete();

                    //Console.WriteLine(repo.GetCount());
                    //using (var dis = bootstrapper.ServiceProvider.GetService<Data.IDataFilter>().Disable<Data.ISoftDelete>())
                    //{
                    //    Console.WriteLine(repo.GetCount());
                    //    //Console.WriteLine(repo.GetList().FirstOrDefault()?.Name);
                    //    //Console.WriteLine(repo.GetList().LastOrDefault()?.Name);

                    //}
                    ////repo.Insert(new User
                    ////{
                    ////    Name = "李四",
                    ////    Age = 48,
                    ////    IsDeleted = false,
                    ////});
                    //Console.WriteLine(repo.GetCount());
                    //Console.WriteLine(repo.GetList().FirstOrDefault()?.Name);
                    //Console.WriteLine(repo.GetList().LastOrDefault()?.Name);
                    //uow.Complete();
                }
            }
        }
    }
}
