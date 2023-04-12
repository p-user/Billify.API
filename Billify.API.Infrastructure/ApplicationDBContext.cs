using Billify.API.Common.Models;
using Billify.API.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Infrastructure
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bill> bills { get; set; }
        public DbSet<Bill_Product> bill_products { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Product> products { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bill>().HasKey(e=>e.Id);
            builder.Entity<Product>().HasKey(e=>e.Id);
            builder.Entity<Client>().HasKey(e=>e.Id);
            builder.Entity<Bill>().HasOne(e => e.client);
            builder.Entity<Bill_Product>().HasKey(sc => new { sc.billId, sc.productId });
            builder.Entity<Bill_Product>().HasOne<Bill>(sc => sc.bill).WithMany(s => s.bill_products).HasForeignKey(sc => sc.Id);
            builder.Entity<Bill_Product>().HasOne<Product>(sc => sc.product).WithMany(s => s.bill_products).HasForeignKey(sc => sc.Id);
            base.OnModelCreating(builder);
        }

    }
      
}
