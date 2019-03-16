using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.AspNetCore;
using Scorpio.AspNetCore.Mvc;
using Scorpio.Modularity;
using Scorpio.Auditing;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Scorpio.WebApplication
{
    [DependsOn(typeof(AspNetCoreMvcModule))]
    public class StartupModule : ScorpioModule
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterAssemblyByConvention();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.Configure<AuditingOptions>(options => options.EnableAuditingPage());
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<Authorization.Permissions.PermissionOptions>(options =>
            {
                options.DefinitionProviders.Add<PermissionProvider>();
                options.GrantingProviders.Add<UserBasePermissionGrantingProvider>();
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            ConfigureServices(context.Services);
        }

        public override void Shutdown(ApplicationShutdownContext context)
        {
            base.Shutdown(context);
        }

        public override void Initialize(ApplicationInitializationContext context)
        {
            
            base.Initialize(context);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMiddleware<Middleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseAuditing();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
