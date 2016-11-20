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

        public void RecreateDb()
        {
            db.Database.EnsureDeleted();
            db.SaveChanges();
            EnsureCreated();
        }

        public void EnsureCreated()
        {
            db.Database.EnsureCreated();
            db.SaveChanges();
        }

        private void Seed()
        {

        }
    }
}
