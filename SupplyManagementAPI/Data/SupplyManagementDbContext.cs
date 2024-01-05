using System;
using SupplyManagementAPI.Models;
using SupplyManagementAPI.Utilities.Enums;
using SupplyManagementAPI.Utilities.Handler;
using System.Data.Entity;
using System.Configuration;

namespace SupplyManagementAPI.Data
{
    public class SupplyManagementDbContext : DbContext
    {
        public SupplyManagementDbContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasKey(c => c.Guid);

            modelBuilder.Entity<Vendor>()
                .HasKey(v => v.Guid);

            modelBuilder.Entity<Account>()
                .HasKey(a => a.Guid);

            modelBuilder.Entity<Company>()
                .HasOptional(c => c.Vendor)
                .WithRequired(v => v.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasOptional(c => c.Account)
                .WithRequired(a => a.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(ar => ar.Accounts)
                .WithRequired(e => e.Role)
                .HasForeignKey(ar => ar.RoleGuid)
                .WillCascadeOnDelete(false);

        }

    }
}
