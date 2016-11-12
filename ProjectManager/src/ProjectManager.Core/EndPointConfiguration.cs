using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Core
{
    public abstract class EndPointConfiguration : IEndPointConfiguration
    {
        public string API_Name { get; set; }
        public string ConnectionString { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public EndPointConfiguration()
        {
            //Parameters = new Dictionary<string, string>();
        }
    }

    public class InProcessEndPoint : EndPointConfiguration { }
    public class RESTEndPoint : EndPointConfiguration { }
    public class WCFEndPoint : EndPointConfiguration { }


    // This class is used for deserializing EndPoints defined in appsettings
    public class EndPointConfigurationTemplate : EndPointConfiguration
    {
        public bool IsActive { get; set; }
        public EndPointType EndPointType { get; set; }
    }
}
