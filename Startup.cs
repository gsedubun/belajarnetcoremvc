using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using belajarnetcoremvc.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace belajarnetcoremvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<belajarDbContext>(opt=>{
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt=> {
                    opt.LoginPath= new Microsoft.AspNetCore.Http.PathString("/security/login") ;
                    opt.LogoutPath= new Microsoft.AspNetCore.Http.PathString("/security/signout") ;
                    opt.ExpireTimeSpan=TimeSpan.FromMinutes(2);
                    opt.SlidingExpiration=false;
                    opt.AccessDeniedPath="/Security/denied";
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();          
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
         
        }
    }
}
