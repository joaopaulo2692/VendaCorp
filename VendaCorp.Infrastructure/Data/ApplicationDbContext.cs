using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaCorp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VendaCorp.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ShippingCompany> ShippingCompanies{ get; set; }
        public DbSet<SalesOrder> SalesOrders{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<SalesOrder>()
            .HasOne(x => x.ShippingCompany)
            .WithMany(x => x.SalesOrders);

            modelBuilder.Entity<Enterprise>()
            .HasMany(x => x.Orders)
            .WithOne(x => x.Enterprise)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
            .HasOne(x => x.SalesOrder)
            .WithOne(x => x.Order)
            .OnDelete(DeleteBehavior.Cascade);

        }


     }
}
