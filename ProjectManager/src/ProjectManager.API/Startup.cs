using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ProjectManager.Core;
using ProjectManager.Domain;
using ProjectManager.Gateway;
using ProjectManager.Services;

namespace ProjectManager.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = ConfigManager.GetConfigurationRoot();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // http://asp.net-hacker.rocks/2016/03/21/configure-aspnetcore.html
            // Add framework services.
            services.AddMemoryCache();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddMvc();
            services.AddCors();
            var formatterSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            formatterSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            formatterSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            JsonOutputFormatter formatter = new JsonOutputFormatter(formatterSettings, System.Buffers.ArrayPool<char>.Shared);

            services.Configure<MvcOptions>(options =>
            {
                options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                options.OutputFormatters.Insert(0, formatter);
            });
            services.Configure<List<EndPointConfiguration>>(Configuration.GetSection("EndPointConfigurations"));

            // Autofac
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ProjectManager.Core.IOCModule());
            builder.RegisterModule(new ProjectManager.Services.IOCModule());
            builder.RegisterModule(new ProjectManager.Gateway.IOCModule());
            builder.Populate(services);
            AutofacRegistrationHelper registrationHelper = new AutofacRegistrationHelper(builder);
            registrationHelper.RegisterEndPoints(ConfigManager.EndPoints);
            registrationHelper.RegisterService<UsersService, IUsersService>(EndPointType.InProcess, APIName.ProjectManager.ToString());
            registrationHelper.RegisterService<DatabaseUtilitiesServicecs, IDatabaseUtilitiesService>(EndPointType.InProcess, APIName.ProjectManager.ToString());
            var container = builder.Build();

            // Make sure the database exists
            //IServiceClient<IDatabaseUtilitiesService> utilities = container.Resolve<IServiceClient<IDatabaseUtilitiesService>>();
            //utilities.Try(x => x.RecreateDb());

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseSession();
            app.UseCors(x => x.WithOrigins(new string[] { "http://localhost:51513" }));
            app.UseMvc();
        }
    }
}
