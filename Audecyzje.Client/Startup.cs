using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.Core.Repositories;
using Audecyzje.Infrastructure.DatabaseContext;
using Audecyzje.Infrastructure.Mappers;
using Audecyzje.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Audecyzje.Infrastructure.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Audecyzje.Client
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString("N")), ServiceLifetime.Singleton);
            //                Configuration.GetSection("ConnectionStrings")["DatabaseServer"],
            //                c => c.MigrationsAssembly("Audecyzje.API")), ServiceLifetime.Transient);

            // Add framework services.
            services.AddMvc();
            services.AddTransient(c => AutoMapperConfig.Initialize());
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            
            //services.Scan(selector =>
            //{
            //    selector.FromAssemblyOf<Service>().AddClasses().AsImplementedInterfaces().WithTransientLifetime();
            //});

            services.AddCors(x =>
            {
                x.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.AllowCredentials();
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
                options.SwaggerDoc("v1", new Info { Title = "Audecyzje API", Version = "v1" });
            });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("AllowAll");
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audecyzje API V1");
            });

            var dbContext = serviceProvider.GetService<AppDbContext>();
            AppDbContextInMemory.Seed(dbContext);
        }
    }
}
