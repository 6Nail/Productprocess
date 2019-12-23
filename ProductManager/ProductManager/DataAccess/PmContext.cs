using ProductManager.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataAccess
{
    public class PmContext : DbContext
    {
        public PmContext() : base("DbConnection")
        {
            Database.SetInitializer(new DbInit<PmContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
