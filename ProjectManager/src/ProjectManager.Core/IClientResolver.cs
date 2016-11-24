using System;
using System.Collections.Generic;
using Autofac;
using ProjectManager.Core;

namespace ProjectManager.Core
{
    public interface IClientResolver<T> 
    {
        T ResolveClient();
    }
}