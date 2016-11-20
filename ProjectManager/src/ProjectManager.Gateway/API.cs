using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class API : IAPI
    {
        public string API_Name { get { return _api_name; } }
        public IList<IEndPointConfiguration> EndPoints { get { return _endPoints; } }

        private string _api_name;
        private IList<IEndPointConfiguration> _endPoints;

        public API(string api_name, IList<IEndPointConfiguration> endPoints)
        {
            _api_name = api_name;
            _endPoints = endPoints.OrderBy(x => x.Preference).ToList();
        }
    }
}
