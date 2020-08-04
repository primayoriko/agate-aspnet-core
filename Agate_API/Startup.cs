using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agate_Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql;
using Swashbuckle.AspNetCore;

namespace Agate_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SchoolContext>(options =>
            {
                if (_env.IsDevelopment())
                {
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]};password={Configuration["dbpass"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("Agate_API"));
                }
                else
                {
                    var connnectionStrings = $"{Configuration["ConnectionStrings:DefaultConnection"]}";
                    options.UseMySql(connnectionStrings, b => b.MigrationsAssembly("Agate_API"));
                }
            });
            
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AgateSwagger",
                    Description = "API Documentation for Agate Project Training",
                    TermsOfService = new Uri("http://github.com/primayoriko/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Naufal Prima Yoriko",
                        Email = "primayoriko@gmail.com",
                        Url = new Uri("http://primayoriko.github.io"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "AGATE",
                        Url = new Uri("https://agate.id"),
                    }
                });
            });
            /*var builder = new SqlConnectionStringBuilder(
                Configuration.GetConnectionString("Movies"));
            builder.Password = Configuration["DbPassword"];
            _connection = builder.ConnectionString;*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //_env.EnvironmentName = "Development";
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            /*app.UseSwagger(c =>
            {
                c.RouteTemplate = "agateswagger/swagger/{documentName}/swagger.json";
            });*/

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgateSwagger");
                //c.SwaggerEndpoint("/agateswagger/swagger/v1/swagger.json", "AgateSwagger");
                //c.RoutePrefix = "agateswagger/swagger";
                //c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
