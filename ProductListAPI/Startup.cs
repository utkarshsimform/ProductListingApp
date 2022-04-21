using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ProtoType.Model;
using ProtoType.Repository;
using ProtoType.Repository.Interfaces;
using ProtoType.Service;
using ProtoType.Service.Interfaces;
using ProtoTypeAPI.Helper;
using ProtoTypeAPI.Middleware;

namespace ProtoTypeAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                    opts.SerializerSettings.Converters.Add(new TrimmingConverter());
                })
                .AddXmlSerializerFormatters();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:40837", "http://localhost:46329")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            services.AddDbContext<IProtoTypeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"),
                //b => b.MigrationsAssembly(typeof(IProtoTypeContext).Assembly.FullName)));
                b => b.MigrationsAssembly("ProductListAPI")));

            services.AddControllers();
            services.AddTransient<IBrandAndProductRepository, BrandAndProductRepository>();
            services.AddTransient<IBrandAndProductService>(sp => new BrandAndProductService(
                sp.GetRequiredService<IBrandAndProductRepository>(),
                sp.GetRequiredService<IProtoTypeContext>()
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IProtoTypeContext dataContext)
        {
            dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowOrigin");
                        
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/Error");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
