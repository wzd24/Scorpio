using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scorpio.Auditing;

namespace Scorpio.WebApplication.Pages
{
    [Authorize("Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
