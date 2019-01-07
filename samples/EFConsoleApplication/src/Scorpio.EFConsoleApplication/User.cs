using Scorpio.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EFConsoleApplication
{
    public class User:Domain.Entities.Entity<int>,ISoftDelete
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public User()
        {
        }
    }
}
