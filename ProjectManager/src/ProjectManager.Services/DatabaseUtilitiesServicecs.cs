using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProjectManager.Domain;

namespace ProjectManager.Services
{
    public class DatabaseUtilitiesServicecs : BaseService, IDatabaseUtilitiesService
    {
        public override APIName APIName { get { return APIName.ProjectManager; } }

        public DatabaseUtilitiesServicecs(Db db) : base(db)
        {

        }

        public void CreateOrUpdateDatabase()
        {
            bool dbExists = (db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

            if (!dbExists)
            {
                db.Database.EnsureCreated();
                Seed();
            }
        }

        private void Seed()
        {

        }
    }
}
