using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;
using Autofac;

namespace ProjectManager.Gateway
{
    public class RegistrationHelper //: IRegistrationHelper
    {
        private Dictionary<Type, IAPI> APIDict;
        private Dictionary<string, IAPI> EndPointDict;

        public RegistrationHelper()
        {
            APIDict = new Dictionary<Type, IAPI>();
            EndPointDict = new Dictionary<string, IAPI>();
        }

        public void RegisterEndPoints(IEnumerable<IEndPointConfiguration> endPoints)
        {
            foreach (var api in endPoints.GroupBy(x => x.API_Name))
                EndPointDict.Add(api.Key, new API(api.Key, api.ToList()));
        }

        public void RegisterAPI(Type serviceType, string api_name)
        {
            IAPI api;
            EndPointDict.TryGetValue(api_name, out api);

            if (api == null)
                throw new Exception($"An API named {api_name} was not found.  Call the RegisterEndPoints method before calling RegisterService with an API name. Also check your spelling.");

            RegisterAPI(serviceType, api);
        }

        public void RegisterAPI(Type serviceType, IAPI api)
        {
            APIDict[serviceType] = api;
        }
    }
}
