using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public class ProjectManagerServiceGateway<T> : ServiceGateway<T> where T:class, IDisposable
    {

        public override string[] EndPointNames
        {
            get
            {
                throw new NotImplementedException();
            }

            protected set
            {
                throw new NotImplementedException();
            }
        }

        public ProjectManagerServiceGateway(ILifetimeScope container, INetworkUtilities networkUtilities) : base(container, networkUtilities)
        {

        }
    }
}
