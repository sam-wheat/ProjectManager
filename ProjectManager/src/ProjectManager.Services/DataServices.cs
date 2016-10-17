using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Model;
using System.Data.Entity;
using ProjectManager.BusinessLogic.Migrations;

namespace ProjectManager.BusinessLogic.Services
{
    public partial class DataServices : IServices
    {
        private readonly Db db;

        public static void SetDatabaseInitializer()
        {
            Database.SetInitializer<Db>(new DBInitializer());
        }

        public DataServices(string _connectionString)
        {
            db = new Db(_connectionString);
        }

        #region IDisposable implementation


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        #endregion
    }
}
