﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Builder;

namespace ProjectManager.Core
{
    public interface IRegistrationHelper
    {
        void RegisterEndPoints(IEnumerable<IEndPointConfiguration> endPoints);
        IRegistrationBuilder<TService, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterService<TService, TInterface>(EndPointType endPointType, string endPointCollectionName);     
    }
}
