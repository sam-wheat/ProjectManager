using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.Principal;

namespace ProjectManager.Core
{
    public class NetworkUtilities : INetworkUtilities
    {
        public bool IsConnectionStringValid(string connectionString)
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
        public bool IsNetworkAvailable()
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
        public bool VerifyDBServerConnectivity(string connectionString)
        {
            bool success = false;

            if (IsNetworkAvailable())
            {
                success = IsConnectionStringValid(connectionString);
            }
            return success;
        }
    }
}