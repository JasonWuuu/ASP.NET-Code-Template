using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Model.Models
{
    public class RapidDbContext : DbContext
    {
        //Development only: initialize the database
        static RapidDbContext()
        {
            Database.SetInitializer(new RapidDatabaseInitializer());
        }

        public RapidDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
