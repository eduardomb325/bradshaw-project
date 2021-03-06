using BradshawProject.Domain.Repositories;
using BradshawProject.Domain.Repositories.Context;
using BradshawProject.Domain.Repositories.Interface;
using BradshawProject.Domain.Repositories.Interfaces;
using BradshawProject.Domain.Services;
using BradshawProject.Domain.Services.Interfaces;
using BradshawProject.Services;
using BradshawProject.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BradshawProject
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
            services.AddControllers()
                    .AddNewtonsoftJson(x =>
                        {
                            x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        }
                    );

            // Using In-Memory DB
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BradshawProject"));

            //Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();

            //Adding Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Bradshaw Project API",
                        Version = "1.0.0",
                        Description = "Bradshaw Project API"
                    }
                );
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            //Use Swagger JSON (like swagger, version, swagger.json)
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Bradshaw Project API");
            }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
