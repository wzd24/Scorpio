using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
using Scorpio.Authorization.Permissions;

namespace Scorpio.ConsoleApplication
{
    [DependsOn(typeof(Authorization.AuthorizationModule))]
    public sealed class ApplicationModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<PermissionOptions>(options =>
            {
                options.DefinitionProviders.Add<PermissionProvider>();
                options.GrantingProviders.Add<UserBasePermissionGrantingProvider>();
            });
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
