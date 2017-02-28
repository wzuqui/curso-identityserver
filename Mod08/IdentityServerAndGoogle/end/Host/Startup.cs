﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4.Quickstart.UI;
using IdentityServer4;

namespace WebApplication4
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer()
                    .AddTemporarySigningCredential()
                    .AddInMemoryClients(Config.Clients())
                    .AddInMemoryIdentityResources(Config.IdentityResources())
                    .AddInMemoryApiResources(Config.ApiResources())
                    .AddTestUsers(TestUsers.Users);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "10786286019-bmt07q6jlkvefql1o2ii87gssj7a4hdd.apps.googleusercontent.com",
                ClientSecret = "sIlbcGotG8okmjOzL0H2g42W"
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
