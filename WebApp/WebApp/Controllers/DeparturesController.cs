﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Dto;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Departure")]
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

        [Route("PostLineSchedule")]
        // POST: api/Departures
        [ResponseType(typeof(Departure))]
        public IHttpActionResult PostLineSchedule([FromBody]ScheduleLine sl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int idd;
            if (sl.Day == "Work day")
                idd = 1;
            else
                idd = 2;

            Day dd = new Day { IDDay = idd, KindOfDay = sl.Day };
            Departure d = new Departure { IDDay = idd, Time = sl.Time,Day = dd };
            if(d.Lines == null)
            {
                d.Lines = new List<Line>();
            }
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == sl.Number);
            line.Stations = new List<Station>();
            d.Lines.Add(line);

            db.Departures.Add(d);

            line.Departures.Add(d);
            db.Lines.Update(line);
            db.Complete();

            return Ok();
        }


        [Route("EditLineSchedule")]
        // POST: api/Departures
        [ResponseType(typeof(Departure))]
        public IHttpActionResult EditLineSchedule([FromBody]ScheduleLine sl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int idd;
            if (sl.Day == "Work day")
                idd = 1;
            else
                idd = 2;

            Day dd = new Day { IDDay = idd, KindOfDay = sl.Day };
            Departure d = new Departure { IDDay = idd, Time = sl.Time, Day = dd };
            if (d.Lines == null)
            {
                d.Lines = new List<Line>();
            }
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == sl.Number);
            line.Stations = new List<Station>();
            d.Lines.Add(line);

            var departure = db.Departures.GetAll().FirstOrDefault(u => u.IDDeparture == sl.IDDay);
            
            db.Departures.Add(d);
            line.Departures.Remove(departure);
            line.Departures.Add(d);
            db.Lines.Update(line);
            db.Complete();

            return Ok();
        }

        [Route("DeleteLineSchedule/{Number}/{IDDay}")]
        // DELETE: api/Departures/5
        [ResponseType(typeof(Departure))]
        public IHttpActionResult DeleteLineSchedule(string Number, int IDDay)
        {
            int id = 0;
            Departure departure = db.Departures.Get(IDDay);
            Line line = db.Lines.Get(Number);
            if (departure == null)
            {
                return NotFound();
            }
            line.Departures.Remove(departure);
            db.Lines.Update(line);
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