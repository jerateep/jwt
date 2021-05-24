using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testrm.Models;

namespace testrm.Data
{
    public class DB_BackOfficeContext : DbContext
    {
        public DB_BackOfficeContext()
        {
        }

        public DB_BackOfficeContext(DbContextOptions<DB_BackOfficeContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.DB_BackOfficeConStr);
            }
        }

    }
}
