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
    public class PropertiesController : ApiController
    {
        private PropertyManagerDbContext db = new PropertyManagerDbContext();

        // GET: api/Properties
        public IEnumerable<PropertyModel> GetProperties()
        {          
            return Mapper.Map<IEnumerable<PropertyModel>>(db.Properties);
        }

        // GET: api/Properties/5
        [ResponseType(typeof(PropertyModel))]
        public IHttpActionResult GetProperty(int id)
        {           
            Property dbProperty = db.Properties.Find(id);
            if (dbProperty == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PropertyModel>(dbProperty));
        }

        // PUT: api/Properties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, PropertyModel property)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                return BadRequest();
            }

            if (!PropertyExists(id))
            {
                return BadRequest();
            }

            // Get the property record corresponding to the property ID, then
            //   update its properties to the values in the input PropertyModel object,
            //   and then set indicator that the record has been modified
            var dbProperty = db.Properties.Find(id);
            dbProperty.Update(property);
            db.Entry(dbProperty).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Unable to update the property in the database.");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Properties
        [ResponseType(typeof(PropertyModel))]
        public IHttpActionResult PostProperty(PropertyModel property)
        {
            // Validate the request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Set up new Property object,
            //  and populate it with the values from 
            //  the input PropertyModel object
            Property dbProperty = new Property();
            dbProperty.Update(property);

            // Add the new Property object to the list of Property objects
            db.Properties.Add(dbProperty);

            // Save the changes to the DB
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Unable to add the property to the database.");
            }

            // Update the PropertyModel object with the new property ID
            //  that was placed in the Property object after the changes
            //  were saved to the DB
            property.PropertyId = dbProperty.PropertyId;
            return CreatedAtRoute("DefaultApi", new { id = dbProperty.PropertyId }, property);
        }

        // DELETE: api/Properties/5
        [ResponseType(typeof(PropertyModel))]
        public IHttpActionResult DeleteProperty(int id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }
          
            try
            {
               /* Remove the leases corresponding to the property
                var leases = db.Leases.Where(l => l.PropertyId == property.PropertyId);
                if (leases != null)
                {
                    //db.Leases.RemoveRange(leases);
                    //db.SaveChanges();
                    foreach (var lease in leases)
                    {
                        db.Leases.Remove(lease);
                        db.SaveChanges();
                    }                   
                }
                */
                // Remove the property
                db.Properties.Remove(property);
                db.SaveChanges();
            }
            catch (Exception)
            {
                //Console.WriteLine("Error deleting: " + e.Message);
                throw new Exception("Unable to delete the property from the database.");
            }

            return Ok(Mapper.Map<PropertyModel>(property));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}