using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.Infrastructure;
using Audecyzje.WebQuickDemo.Data;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audecyzje.WebQuickDemo
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
            services.AddMvc();
            services.AddDbContext<WarsawContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString("N")), ServiceLifetime.Singleton);
            services.AddSingleton<RegularJobs.DecisionQueuer>();
            //services.AddDbContext<WarsawContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.CommandTimeout(60).EnableRetryOnFailure()));
            
            //TODO uncomment and debug
            //TODO we should add some logs of documents processing
            //RecurringJob.AddOrUpdate<RegularJobs.DecisionQueuer>(x => x.CheckForNewDecisions(), Cron.Daily);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Decisions}/{action=Index}/{id?}");
            });
        }
    }
}
