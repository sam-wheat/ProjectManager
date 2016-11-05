﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Services.Integration
{
    public class DropAndRecreateInitializer
    {
        private SQLServerDbContextOptions _options;
        private Db _db;

        public DropAndRecreateInitializer(SQLServerDbContextOptions options)
        {
            _options = options;
            _db = new Db(_options);
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