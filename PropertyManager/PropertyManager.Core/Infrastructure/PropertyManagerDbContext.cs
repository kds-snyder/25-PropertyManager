using PropertyManager.Core.Domain;
using PropertyManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManager.Core.Infrastructure
{
    public class PropertyManagerDbContext : DbContext
    {
        public PropertyManagerDbContext() : base("PropertyManager")
        {
        }

        public IDbSet<Property> Properties { get; set; }
        public IDbSet<Tenant> Tenants { get; set; }
        public IDbSet<Lease> Leases { get; set; }
        public IDbSet<Image> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(p => p.PropertyId);
            modelBuilder.Entity<Property>().HasMany(l => l.Leases)
                                           .WithRequired(p => p.Property)
                                           .HasForeignKey(p => p.PropertyId);
            modelBuilder.Entity<Property>().HasMany(i => i.Images)
                                           .WithRequired(p => p.Property)
                                           .HasForeignKey(p => p.PropertyId);

            modelBuilder.Entity<Tenant>().HasKey(t => t.TenantId);
            modelBuilder.Entity<Tenant>().HasMany(l => l.Leases)
                                           .WithRequired(t => t.Tenant)
                                           .HasForeignKey(p => p.TenantId);

            modelBuilder.Entity<Lease>().HasKey(l => l.LeaseId);

            modelBuilder.Entity<Image>().HasKey(i => i.ImageId);
        }
    }
}
