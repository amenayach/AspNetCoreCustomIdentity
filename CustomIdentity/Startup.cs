using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomIdentity.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using  CustomIdentity.Middlewares;

namespace CustomIdentity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var userStore = new UserStore();
            services.AddSingleton<IUserStore<ApplicationUser>>(userStore);
            services.AddSingleton<IRoleStore<ApplicationRole>>(userStore);
            services.AddSingleton<IUserPasswordStore<ApplicationUser>>(userStore);

            services.AddAuthentication();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigin",
            //        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //});

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            //});

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {

            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors(options =>
            //{
            //    options.AllowAnyHeader();
            //    options.AllowAnyMethod();
            //    options.AllowAnyOrigin();
            //});
            app.UseOptionMiddleware();

            //app.UseAuthentication();
            app.UseIdentity();

            app.UseMvc();
        }
    }
}
