using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Core;

namespace ProjectManager.Services.Integration
{
    public class DropAndRecreateInitializer
    {
        private Db _db;

        public DropAndRecreateInitializer(Db db)
        {
            _db = db;
        }

        public void DropAndRecreateDb()
        {
            _db.Database.EnsureDeleted();
            _db.SaveChanges();
            EnsureCreated();
        }

        public void EnsureCreated()
        {
            _db.Database.EnsureCreated();
            _db.SaveChanges();
        }
    }
}
