using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.Principal;

namespace ProjectManager.Core
{
    public class Utilities
    {
        public static bool IsConnectionStringValid(string connectionString)
        {
            bool result = true;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }


        /// <summary>
        /// Verifies a network interface exists. 
        /// </summary>
        /// <returns></returns>
        public static bool VerifyNetworkConnectivity()
        {
            bool success = NetworkInterface.GetIsNetworkAvailable();
            return success;
        }


        /// <summary>
        /// Verifies basic network connectivity 
        /// and that the user is connected to the LAN and has a valid windows security credential 
        /// and that valid connection strings exist and that the DB server is up and running and accepting connections.
        /// </summary>
        /// <returns></returns>
        public static bool VerifyDBServerConnectivity(string WindowsDomainName, string connectionString)
        {
            bool success = false;

            if (VerifyNetworkConnectivity())
            {
                if (WindowsIdentity.GetCurrent().Name.ToUpper().Contains(WindowsDomainName.ToUpper()))
                    success = Utilities.IsConnectionStringValid(connectionString);
            }
            return success;
        }

        /// <summary>
        /// Escapes embeded quotes in strings for javascript
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UnQuote(string s)
        {
            var t = s.Replace("\"", "&quot;").Replace("'", "\\'");
            return t;
        }
    }
}