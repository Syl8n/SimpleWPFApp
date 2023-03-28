using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _301004212_Suh__ASS4.entity;
using Microsoft.EntityFrameworkCore;

namespace _301004212_Suh__ASS4
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }

        public static string MDF_Directory
        {
            get
            {
                var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                return Path.GetFullPath(Path.Combine(directoryPath, "..//..//..//.."));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; "
                + "AttachDbFilename=" + MDF_Directory + "\\Customer.mdf; "
                + "Initial Catalog=Customer");
        }
    }
}
