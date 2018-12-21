using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Scorpio;
namespace Scorpio.EFConsoleApplication
{
    class DemoOnSaveChangeHandler : IOnSaveChangeHandler
    {
        private readonly ILogger<DemoOnSaveChangeHandler> _logger;

        public DemoOnSaveChangeHandler(ILogger<DemoOnSaveChangeHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public async Task PostSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public async Task PreSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            foreach (var item in entries)
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item.Entity));
            }

        }
    }
}
