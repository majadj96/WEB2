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
    [Authorize]
    [RoutePrefix("api/Station")]
    public class StationsController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IUnitOfWork db;
        public StationsController(IUnitOfWork db)
        {
            this.db = db;
        }
        [Authorize(Roles = "Admin")]
        [Route("GetAll")]
        public IEnumerable<Station> GetStations()
        {
            return db.Stations.GetAll();
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateStation")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStation([FromBody]Station station)
        {
            int result = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stations.Update(station);

            result= db.Complete();

            if (result == 0)
            {
                return Conflict();
            }
            


            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Admin")]
        [Route("Add")]
        [ResponseType(typeof(Station))]
        public IHttpActionResult PostStation([FromBody]Station station)
        {
            int result = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stations.Add(station);
            
            try
            {
               result = db.Complete();
            }
            catch (Exception)
            {
                if (StationExists(station.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            if (result == 0)
            {
                return Conflict();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("Delete/{id}/")]
        [ResponseType(typeof(Station))]
        public IHttpActionResult DeleteStation(string id)
        {
            Station station = db.Stations.Get(id);
            if (station == null)
            {
                return NotFound();
            }

            db.Stations.Remove(station);
            db.Complete();

            return Ok(station);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StationExists(string id)
        {
            return db.Stations.GetAll().Count(e => e.Name == id) > 0;
        }
    }
}