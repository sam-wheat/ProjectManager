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
        public static string AppRootDir { get { return PlatformServices.Default.Application.ApplicationBasePath; } }

        /// <summary>
        /// Contains artifacts and resources for current app config i.e. DEV or PROD.  Data and Log directories live here.
        /// </summary>
        /// 
        public static string AppCurrentConfigDir { get { return Path.Combine(AppRootDir, CurrentEnvironmentName); } }

        /// <summary>
        /// Directory where data files reside for current configuration 
        /// </summary>
        public static string AppDataDir { get { return Path.Combine(AppCurrentConfigDir, "Data"); } }

        /// <summary>
        /// Directory where log files reside for current configuration i.e. DEV or PROD
        /// </summary>
        public static string AppLogDir { get { return Path.Combine(AppCurrentConfigDir, "Logs"); } }

        /// <summary>
        /// Returns a valid connection string OR a URL to a service
        /// </summary>
        public static string ConnectionString { get { return _ConnectionString; } }
        
        /// <summary>
        /// Returns the name of the connectionstring as defined in the config file.
        /// </summary>
        public static string ConnectionStringName { get { return _ConnectionStringName; } }


        public static string CurrentEnvironmentName { get { return _CurrentEnvironmentName; } }

        private static string _CurrentEnvironmentName;
        private static string _ConnectionStringName = "ProjectManagerLocal";
        private static string _ConnectionString;

        static DataConfigManager()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            // Set configuration variable.  This variable is used in the directory path for data files and logs so we need to do this first.
#if DEBUG
            _CurrentEnvironmentName = "DEV";
#else
            _CurrentEnvironmentName = "PROD";
#endif
            //AppDomain.CurrentDomain.SetData("DataDirectory", AppDataDir);  // Required so visual studio can transform the |DataDirectory| variable in app.config

            // Verify Directories.  Need to do this first so we have a dir to write a log to.
            VerifyApplicationDirectories();

            // Set database connection strings
            _ConnectionString = config["Data:ConnectionString"];
        }

        private static void VerifyApplicationDirectories()
        {
            if (!Directory.Exists(AppRootDir))
                Directory.CreateDirectory(AppRootDir);

            if (!Directory.Exists(AppCurrentConfigDir))
                Directory.CreateDirectory(AppCurrentConfigDir);


            if (!Directory.Exists(AppDataDir))
                Directory.CreateDirectory(AppDataDir);


            if (!Directory.Exists(AppLogDir))
                Directory.CreateDirectory(AppLogDir);
        }


        
    }
}
