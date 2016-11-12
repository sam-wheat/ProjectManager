using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class EndPointConfigurationKey
    {
        public readonly EndPointType EndPointType;
        public readonly string API_Name;

        public EndPointConfigurationKey(EndPointType endPointType, string apiName)
        {
            if (string.IsNullOrEmpty(apiName))
                throw new ArgumentNullException("apiName");

            EndPointType = endPointType;
            API_Name = apiName;
        }

        public override int GetHashCode()
        {
            return EndPointType.GetHashCode() * API_Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            EndPointConfigurationKey that = obj as EndPointConfigurationKey;
            return this.EndPointType == that.EndPointType && this.API_Name == that.API_Name;
        }
    }
}
