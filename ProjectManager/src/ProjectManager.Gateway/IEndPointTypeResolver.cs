using System;
using ProjectManager.Core;

namespace ProjectManager.Gateway
{
    public interface IEndPointTypeResolver
    {
        void Register(Type serviceType, IAPI api);
        IAPI Resolve(Type serviceType);
    }
}