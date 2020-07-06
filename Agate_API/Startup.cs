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
using Pomelo.EntityFrameworkCore.MySql;

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
