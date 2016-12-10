using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ProjectManager.Core
{
    public static class ConfigManager
    {
        /// <summary>
        /// Root Application directory i.e. C:\Users\SWheat\AppData\CompanyName\ProductName.  Typically you will NOT store stuff here.  Use the AppCurrentConfigDir, AppDataDir or AppLogDir
        /// </summary>
        public static string UserDataDir { get { return Path.Combine(userDataDir, productDataDir); } }
        
        /// <summary>
        /// Directory where app executable lives
        /// </summary>
        public static string AppCurrentDir { get { return Directory.GetCurrentDirectory(); } }

        /// <summary>
        /// Contains artifacts and resources for current app config i.e. DEV or PROD.  Data and Log directories live here.
        /// </summary>
        public static string AppEnvironmentDir { get { return Path.Combine(UserDataDir, EnvironmentName); } }

        /// <summary>
        /// Directory where data files reside for current configuration 
        /// </summary>
        public static string AppDataDir { get { return Path.Combine(AppEnvironmentDir, "Data"); } }
        
        /// <summary>
        /// Directory where log files reside for current configuration i.e. DEV or PROD
        /// </summary>
        public static string AppLogDir { get { return Path.Combine(AppEnvironmentDir, "Logs"); } }

        /// <summary>
        /// Returns a valid connection string OR a URL to a service
        /// </summary>
        public static string ConnectionString { get; private set;  }
        
        /// <summary>
        /// Returns the name of the connectionstring as defined in the config file.
        /// </summary>
        public static string ConnectionStringName { get; private set; }

        /// <summary>
        /// Returns the standardized name of the environment as defined in the ASPNETCORE_ENVIRONMENT variable.
        /// </summary>
        public static string EnvironmentName
        {
            get
            {
                if (string.IsNullOrEmpty(environmentName))
                    environmentName = GetEnvName();

                return environmentName;
            }
                
        }
        /// <summary>
        /// Returns the list of EndPoints defined in the config file.
        /// </summary>
        public static IEnumerable<IEndPointConfiguration> EndPoints { get; private set; }
        private static string environmentName;
        private static string productDataDir;
        private static string userDataDir;

        static ConfigManager()
        {
            IConfigurationRoot config = GetConfigurationRoot();
            userDataDir = Environment.GetEnvironmentVariable("LocalAppData");
            productDataDir = config["Config:ProductDataDir"]; // do not use leading "\" in appsettings
            ConnectionStringName = config["Config:CurrentConnectionString"];
            ConnectionString = (config["ConnectionStrings:" + ConnectionStringName]).Replace("{DataDirectory}", AppDataDir);
            EndPoints = new List<IEndPointConfiguration>();
            List<EndPointConfiguration> endPoints = new List<EndPointConfiguration>();
            config.GetSection("EndPointConfigurations").Bind(endPoints);


            if (endPoints == null)
                return;
            else
                endPoints = endPoints.Where(x => x.IsActive).ToList();

            if (endPoints.Any(x => string.IsNullOrEmpty(x.Name)))
                throw new Exception("One or more EndPointConfigurations has a blank name.  Name is required for all EndPointConfigurations");

            if (endPoints.Any(x => string.IsNullOrEmpty(x.Collection_Name)))
                throw new Exception("One or more EndPointConfigurations has a blank Collection name.  Collection Name is required for all EndPointConfigurations");

            var dupes = endPoints.GroupBy(x => new { x.Name }).Where(x => x.Count() > 1);

            if (dupes.Any())
                throw new Exception($"Duplicate EndPointConfiguration found. EndPoint Name: {dupes.First().Key.Name}." + Environment.NewLine + "Each EndPoint must have a unique name.  Set the Active flag to false to bypass an EndPoint.");

            EndPoints = endPoints;
        }

        public static IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppCurrentDir)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private static string GetEnvName()
        {
            string env = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development").ToLower();

            if (env.Contains("dev"))
                return "Development";
            else if (env.Contains("prod"))
                return "Production";
            else
                throw new Exception(string.Format("ASPNETCORE_ENVIRONMENT setting not recognized: {0}", env));
        }
    }
}
