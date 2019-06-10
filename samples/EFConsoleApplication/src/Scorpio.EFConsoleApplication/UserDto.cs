using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Scorpio.EFConsoleApplication
{
    public class UserDto:Application.Dtos.EntityDto<int>
    {
        [MaxLength(30)]

        public string Name { get; set; }

        [MaxLength(50)]
        public string NickName { get; set; }

        public int Age { get; set; }
    }
}
