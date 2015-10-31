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
    public class LeasesController : ApiController
    {
        private PropertyManagerDbContext db = new PropertyManagerDbContext();

        // GET: api/Leases
        public IEnumerable<LeaseModel> GetLeases()
        {
            return  Mapper.Map<IEnumerable<LeaseModel>>(db.Leases);
        }

        // GET: api/Leases/5
        [ResponseType(typeof(LeaseModel))]
        public IHttpActionResult GetLease(int id)
        {
            Lease dbLease = db.Leases.Find(id);
            if (dbLease == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<LeaseModel>(dbLease));
        }

        // PUT: api/Leases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLease(int id, LeaseModel lease)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lease.LeaseId)
            {
                return BadRequest();
            }

            if (!LeaseExists(id))
            {
                return BadRequest();
            }

            // Get the lease record corresponding to the lease ID, then
            //   update its properties to the values in the input LeaseModel object,
            //   and then set indicator that the record has been modified
            var dbLease = db.Leases.Find(id);
            dbLease.Update(lease);
            db.Entry(dbLease).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update the lease in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Leases
        [ResponseType(typeof(LeaseModel))]
        public IHttpActionResult PostLease(LeaseModel lease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Set up new Lease object,
            //  and populate it with the values from 
            //  the input LeaseModel object
            Lease dbLease = new Lease();
            dbLease.Update(lease);

            // Add the new Lease object to the list of Lease objects
            db.Leases.Add(dbLease);

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Unable to add the lease to the database.");
            }

            // Update the LeaseModel object with the new lease ID
            //  that was placed in the Lease object after the changes
            //  were saved to the DB
            lease.LeaseId = dbLease.LeaseId;
            return CreatedAtRoute("DefaultApi", new { id = dbLease.LeaseId }, lease);
        }

        // DELETE: api/Leases/5
        [ResponseType(typeof(LeaseModel))]
        public IHttpActionResult DeleteLease(int id)
        {
            Lease lease = db.Leases.Find(id);
            if (lease == null)
            {
                return NotFound();
            }

            db.Leases.Remove(lease);

            try
            {

                db.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Unable to delete the lease from the database.");
            }

            return Ok(Mapper.Map<LeaseModel>(lease));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaseExists(int id)
        {
            return db.Leases.Count(e => e.LeaseId == id) > 0;
        }
    }
}