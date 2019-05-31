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
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class DeparturesController : ApiController
    {  
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IUnitOfWork db;
        public DeparturesController(IUnitOfWork db)
        {
            this.db = db;
        }
        // GET: api/Departures
        public IEnumerable<Departure> GetDepartures()
        {
            return db.Departures.GetAll();
        }

        // GET: api/Departures/5
        [ResponseType(typeof(Departure))]
        public IHttpActionResult GetDeparture(int id)
        {
            Departure departure = db.Departures.Get(id);
            if (departure == null)
            {
                return NotFound();
            }

            return Ok(departure);
        }

        // PUT: api/Departures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeparture(int id, Departure departure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departure.IDDeparture)
            {
                return BadRequest();
            }
            db.Departures.Update(departure);


            try
            {
                db.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departures
        [ResponseType(typeof(Departure))]
        public IHttpActionResult PostDeparture(Departure departure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departures.Add(departure);
            db.Complete();

            return CreatedAtRoute("DefaultApi", new { id = departure.IDDeparture }, departure);
        }

        // DELETE: api/Departures/5
        [ResponseType(typeof(Departure))]
        public IHttpActionResult DeleteDeparture(int id)
        {
            Departure departure = db.Departures.Get(id);
            if (departure == null)
            {
                return NotFound();
            }

            db.Departures.Remove(departure);
            db.Complete();

            return Ok(departure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartureExists(int id)
        {
            return db.Departures.GetAll().Count(e => e.IDDeparture == id) > 0;
        }
    }
}