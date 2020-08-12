using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;

namespace Agate_OData
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILogger Logger { get; }
        public Startup(IConfiguration configuration)//, ILogger logger)
        {
            Configuration = configuration;
            //Logger = logger;
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //var entity = builder.EntitySet<Class>("Classes");
            builder.EntitySet<Class>("Classes").EntityType.HasKey(e => e.Grade);
            //builder.EntitySet<Class>("Classes").EntityType.HasKey(e => new { e.Grade, e.ClassNumber });
            //var entity2 = builder.EntitySet<Student>("Students");
            builder.EntitySet<Student>("Students").EntityType.HasKey(e => e.StudentId);
            return builder.GetEdmModel();
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //System.Diagnostics.Debug.WriteLine("sadasd");
            services.AddDbContext<SchoolContext>(opts =>
            {
                opts.UseInMemoryDatabase("SchoolDB");
            });

            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                //options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            })
                    .AddNewtonsoftJson();

            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Count().Filter().OrderBy().SkipToken().Build();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
    }
}
