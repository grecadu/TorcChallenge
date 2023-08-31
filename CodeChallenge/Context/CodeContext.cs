using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;

namespace CodeChallenge.Context
{
    public class CodeContext : DbContext
    {
        public CodeContext(DbContextOptions<CodeContext> options) : base(options)
        {
        }
        public CodeContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            });
        }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }


    }

}
