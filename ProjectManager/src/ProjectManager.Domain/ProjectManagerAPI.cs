using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Domain
{
    public class ProjectManagerAPI : IAPI
    {
        public string API_Name { get { return APIName.ProjectManager.ToString();  }}

        private IList<EndPointType> endPointPreferences;
        public IList<EndPointType> EndPointPreferences
        {
            get { return endPointPreferences; }
            private set { endPointPreferences = value; }
        }

        public ProjectManagerAPI()
        {
            EndPointPreferences = new List<EndPointType>();
            EndPointPreferences.Add(EndPointType.InProcess);
            EndPointPreferences.Add(EndPointType.REST);
        }
    }
}
