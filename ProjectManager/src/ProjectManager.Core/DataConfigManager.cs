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
    public static class DataConfigManager
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
        public static string AppEnvironmentDir { get { return Path.Combine(UserDataDir, CurrentEnvironmentName); } }

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
        /// Returns the name of the current environment as as defined in the config file.
        /// </summary>
        public static string CurrentEnvironmentName { get; private set; }

        private static string productDataDir;
        private static string userDataDir;

        static DataConfigManager()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(AppCurrentDir);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            userDataDir = Environment.GetEnvironmentVariable("LocalAppData");
            productDataDir = config["Config:ProductDataDir"]; // do not use leading "\" in appsettings
            ConnectionStringName = config["Config:CurrentConnectionString"];
            CurrentEnvironmentName = config["Config:Environment"];
            ConnectionString = (config["ConnectionStrings:" + ConnectionStringName]).Replace("{DataDirectory}", AppDataDir);
            // Verify Directories.  Need to do this first so we have a dir to write a log to.
            VerifyApplicationDirectories();
        }

        private static void VerifyApplicationDirectories()
        {
            if (!Directory.Exists(UserDataDir))
                Directory.CreateDirectory(UserDataDir);

            if (!Directory.Exists(AppEnvironmentDir))
                Directory.CreateDirectory(AppEnvironmentDir);


            if (!Directory.Exists(AppDataDir))
                Directory.CreateDirectory(AppDataDir);


            if (!Directory.Exists(AppLogDir))
                Directory.CreateDirectory(AppLogDir);
        }


        
    }
}
