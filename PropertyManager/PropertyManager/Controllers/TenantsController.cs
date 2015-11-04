using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PropertyManager.Core.Domain;
using PropertyManager.Core.Infrastructure;
using PropertyManager.Core.Models;
using AutoMapper;

namespace PropertyManager.Controllers
{
    [Authorize]
    public class TenantsController : ApiController
    {
        private PropertyManagerDbContext db = new PropertyManagerDbContext();

        // GET: api/Tenants
        public IEnumerable<TenantModel> GetTenants()
        {
            return Mapper.Map<IEnumerable<TenantModel>>(db.Tenants);
        }

        // GET: api/Tenants/5
        [ResponseType(typeof(TenantModel))]
        public IHttpActionResult GetTenant(int id)
        {
            Tenant dbTenant = db.Tenants.Find(id);
            if (dbTenant == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TenantModel>(dbTenant));
        }

        // PUT: api/Tenants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTenant(int id, TenantModel tenant)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tenant.TenantId)
            {
                return BadRequest();
            }

            if (!TenantExists(id))
            {
                return BadRequest();
            }

            // Get the tenant record corresponding to the tenant ID, then
            //   update its properties to the values in the input TenantModel object,
            //   and then set indicator that the record has been modified
            var dbTenant = db.Tenants.Find(id);
            dbTenant.Update(tenant);
            db.Entry(dbTenant).State = EntityState.Modified;           

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update the tenant in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tenants
        [ResponseType(typeof(TenantModel))]
        public IHttpActionResult PostTenant(TenantModel tenant)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Set up new Tenant object,
            //  and populate it with the values from 
            //  the input TenantModel object
            Tenant dbTenant = new Tenant();
            dbTenant.Update(tenant);

            // Add the new Tenant object to the list of Tenant objects
            db.Tenants.Add(dbTenant);

            // Save the changes to the DB
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Unable to add the tenant to the database.");
            }

            // Update the TenantModel object with the new tenant ID
            //  that was placed in the Tenant object after the changes
            //  were saved to the DB
            tenant.TenantId = dbTenant.TenantId;
            return CreatedAtRoute("DefaultApi", new { id = dbTenant.TenantId }, tenant);
        }

        // DELETE: api/Tenants/5
        [ResponseType(typeof(Tenant))]
        public IHttpActionResult DeleteTenant(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return NotFound();
            }           

            try
            {
                
                // Remove the leases corresponding to the tenant
                var leases = db.Leases.Where(l => l.TenantId == tenant.TenantId);
                if (leases != null)
                {
                    //db.Leases.RemoveRange(leases);
                    //db.SaveChanges();
                    foreach (var lease in leases)
                    {
                        db.Leases.Remove(lease);                       
                    }
                    db.SaveChanges();
                }

                // Remove the tenant
                db.Tenants.Remove(tenant);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Unable to delete the tenant from the database.");
            }

            return Ok(Mapper.Map<TenantModel>(tenant));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TenantExists(int id)
        {
            return db.Tenants.Count(e => e.TenantId == id) > 0;
        }
    }
}