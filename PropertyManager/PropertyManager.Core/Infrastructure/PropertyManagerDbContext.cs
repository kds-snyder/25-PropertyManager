﻿using PropertyManager.Core.Domain;
using PropertyManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PropertyManager.Core.Infrastructure
{
    public class PropertyManagerDbContext : IdentityDbContext
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
            modelBuilder.Entity<Property>().HasMany(p => p.Leases)
                                           .WithRequired(l => l.Property)
                                           .HasForeignKey(l => l.PropertyId);

            modelBuilder.Entity<Property>().HasMany(p => p.Images)
                                           .WithRequired(i => i.Property)
                                           .HasForeignKey(i => i.PropertyId);

            modelBuilder.Entity<Tenant>().HasKey(t => t.TenantId);
            modelBuilder.Entity<Tenant>().HasMany(t => t.Leases)
                                           .WithRequired(l => l.Tenant)
                                           .HasForeignKey(l => l.TenantId);

            modelBuilder.Entity<Lease>().HasKey(l => l.LeaseId);

            modelBuilder.Entity<Image>().HasKey(i => i.ImageId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
