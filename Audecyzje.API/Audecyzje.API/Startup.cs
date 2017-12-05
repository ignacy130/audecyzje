using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audecyzje.Core.Repositories;
using Audecyzje.Infrastructure.DatabaseContext;
using Audecyzje.Infrastructure.Mappers;
using Audecyzje.Infrastructure.Repositories;
using Audecyzje.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Audecyzje.API
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["DatabaseServer"],
                c => c.MigrationsAssembly("Audecyzje.API")), ServiceLifetime.Transient);


            services.AddMvc();

            services.AddTransient(c => AutoMapperConfig.Initialize());
            services.AddTransient<IDocumentRepository, DocumentRepository>();

            services.Scan(selector =>
            {
                
                selector.FromAssemblyOf<Service>().AddClasses().AsImplementedInterfaces().WithTransientLifetime();
                
            });
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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop Online API V1");
            });


        }
    }
}
