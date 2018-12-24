using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scorpio.Data;
using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.HostService
{
    public class DemoDbContext : ScorpioDbContext<DemoDbContext>
    {

        public DemoDbContext(IServiceProvider serviceProvider, DbContextOptions<DemoDbContext> contextOptions, IOptions<DataFilterOptions> filterOptions) : base(serviceProvider, contextOptions, filterOptions)
        {
        }
    }
}
