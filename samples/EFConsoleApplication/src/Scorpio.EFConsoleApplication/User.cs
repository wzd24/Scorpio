using Scorpio.Data;
using Scorpio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Scorpio.EFConsoleApplication
{
    public class User :Entity<int>, ISoftDelete, IHasExtraProperties
    {
        [MaxLength(30)]
        
        public string Name { get; set; }

        [MaxLength(50)]
        public string NickName { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public Dictionary<string, object> ExtraProperties { get; protected set; }

        public User()
        {
            ExtraProperties = new Dictionary<string, object>();
        }
    }
}
