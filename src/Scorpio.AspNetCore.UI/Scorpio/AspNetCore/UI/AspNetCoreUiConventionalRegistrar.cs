using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.AspNetCore.Mvc
{
    internal class AspNetCoreUiConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.Services.RegisterAssembly(context.Assembly, config =>
            {
                config.Where(t => t.IsAssignableTo<ITagHelper>()).AsSelf().Lifetime(ServiceLifetime.Transient);
            });
        }
    }
}
