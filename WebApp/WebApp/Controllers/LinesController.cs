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
using WebApp.Dto;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize]
    [RoutePrefix("api/Line")]
    public class LinesController : ApiController
    {
        private IUnitOfWork db;

        public LinesController()
        {

        }

      
        public LinesController(IUnitOfWork db)
        {
            this.db = db;
        }
    
        [AllowAnonymous]
        [Route("GetScheduleLines")]
        public IEnumerable<Line> GetScheduleLines(string typeOfLine)
        {

            if (typeOfLine == null)
            {
                var type = db.TypesOfLine.GetAll().FirstOrDefault(u => u.typeOfLine == "City");
                return db.Lines.GetAll().Where(u => u.IDtypeOfLine == type.IDtypeOfLine);
            }
            else
            {
                var type = db.TypesOfLine.GetAll().FirstOrDefault(u => u.typeOfLine == typeOfLine);
                return db.Lines.GetAll().Where(u => u.IDtypeOfLine == type.IDtypeOfLine);
            }
        }


        [AllowAnonymous]
        [Route("GetSchedule")]
        public string GetSchedule(string typeOfLine, string typeOfDay, string Number)
        {
            var type = db.TypesOfLine.GetAll().FirstOrDefault(u => u.typeOfLine == typeOfLine);
            var day = db.Days.GetAll().FirstOrDefault(u => u.KindOfDay == typeOfDay);
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == Number);

            string dep = "";
            int i = 0;
            foreach(Departure d in line.Departures)
            {
                if (d.IDDay == day.IDDay)
                {
                    i++;
                    dep +=d.Time.Hour.ToString()+":"+d.Time.Minute.ToString()+" ";
                    
                }
            }
            if(line.Departures.Count>0)
            dep = dep.Substring(0, dep.Length - 1);
            List<Departure> deps = new List<Departure>();
            deps = db.Departures.GetAll().Where(u => u.IDDay == day.IDDay).ToList();
            


            return dep;
        }


        [Authorize(Roles = "Admin")]
        [Route("GetScheduleAdmin")]
        public IEnumerable<ScheduleLine> GetScheduleAdmin()
        {
            List<ScheduleLine> schedule = new List<ScheduleLine>();
            var lines = db.Lines.GetAll();
            foreach(var line in lines)
            {
                foreach (var dep in line.Departures)
                {
                    //  Day day = db.Days.GetAll().FirstOrDefault(u => u.IDDay == dep.IDDay);

                    ScheduleLine sl = new ScheduleLine();
                    sl.Number = line.Number;
                    sl.Time = dep.Time;
                    if(dep.IDDay == 1)
                    {
                        sl.Day = "Work day";
                    }
                    else
                    {
                        sl.Day = "Weekend";
                    }
                    sl.IDDay = dep.IDDeparture;

                    schedule.Add(sl);
                }

            }

            return schedule;
            
        }

        [Authorize(Roles = "Admin")]
        [Route("GetLines")]
        // GET: api/Lines
        public IEnumerable<Line> GetLines()
        {
            return db.Lines.GetAll();
        }

        // GET: api/Lines/5
        [ResponseType(typeof(Line))]
        public IHttpActionResult GetLine(string id)
        {
            Line line = db.Lines.Get(id);
            if (line == null)
            {
                return NotFound();
            }

            return Ok(line);
        }

        // PUT: api/Lines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLine(string id, Line line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != line.Number)
            {
                return BadRequest();
            }

            db.Lines.Update(line);

            try
            {
                db.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineExists(id))
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


        [Authorize(Roles = "Admin")]
        [Route("PostLineSchedule")]
        // POST: api/Lines
       // [ResponseType(typeof(Line))]
        public IHttpActionResult PostLine([FromBody]ScheduleLine sl)
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

            Departure d = new Departure { IDDay = idd, Time = sl.Time};
            var line = db.Lines.GetAll().FirstOrDefault(u => u.Number == sl.Number);
            d.Lines.Add(line);

            db.Departures.Add(d);

            line.Departures.Add(d);
            db.Lines.Update(line);

            try
            {
                db.Complete();
            }
            catch (DbUpdateException)
            {
                
            }

            return CreatedAtRoute("DefaultApi", new { id = line.Number }, line);
        }

        // DELETE: api/Lines/5
        [ResponseType(typeof(Line))]
        public IHttpActionResult DeleteLine(string id)
        {
            Line line = db.Lines.Get(id);
            if (line == null)
            {
                return NotFound();
            }

            db.Lines.Remove(line);
            db.Complete();

            return Ok(line);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LineExists(string id)
        {
            return db.Lines.GetAll().Count(e => e.Number == id) > 0;

        }
    }
}