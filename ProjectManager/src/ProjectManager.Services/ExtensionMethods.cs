using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Services
{
    public static class ExtensionMethods
    {
        internal static void AttachAsModfied<T>(this Db context, T entity) where T : class
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        internal static void AttachAsDeleted<T>(this Db context, T entity) where T : class
        {
            context.Set<T>().Attach(entity);
            context.Entry<T>(entity).State = EntityState.Deleted;
        }
    }
}
