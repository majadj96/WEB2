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
    [RoutePrefix("api/Tickets")]
    public class TicketsController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IUnitOfWork db;
        public TicketsController(IUnitOfWork db)
        {
            this.db = db;
        }

        public TicketsController() { }

        [Route("Tickets")]
        // GET: api/Tickets
        public IEnumerable<Ticket> GetTickets()
        {

            var email1 = Request.GetOwinContext().Authentication.User.Identity.Name;


            return db.Tickets.GetAll().Where(t=>t.UserName==email1 && t.IDtypeOfTicket==1);
            
        }

        // GET: api/Tickets/5
        [Route("CheckValidation")]
        [ResponseType(typeof(Ticket))]
        public string GetTicket(int id)
        {
            DateTime dateTime = new DateTime();
            string result = "";
            Ticket ticket = db.Tickets.Get(id);
            if (ticket == null)
            {
                return result;
            }
            //One-hour
            if (ticket.IDtypeOfTicket == 1)
            {

                if (ticket.CheckIn == ticket.BoughtTime)
                {
                    result = "Not checked in yet. Invalid.";

                }else if (ticket.CheckIn > ticket.BoughtTime)
                {
                    dateTime = ticket.CheckIn.AddHours(1) ;

                    if (ticket.CheckIn > dateTime)
                    {
                        result = "1 hour has expired. Invalid.";
                    }else
                    {
                        result = "Valid ticket!";
                    }
                }

            //Day
            }else if (ticket.IDtypeOfTicket == 2)
            {
                dateTime = DateTime.Now;

                if (ticket.BoughtTime == DateTime.Today)
                {
                    result = "Valid ticket!";
                }else
                {
                    result = "Day has expired. Invalid";
                }
                //Mounth
            }
            else if (ticket.IDtypeOfTicket == 3)
            {
                dateTime = DateTime.Now;

                if(ticket.BoughtTime.Month == dateTime.Month && ticket.BoughtTime.Year == dateTime.Year)
                {
                    result = "Valid ticket";
                }else
                {
                    result = "Month has expired. Invalid";
                }
                
            //Year
            }else if (ticket.IDtypeOfTicket == 4)
            {
                dateTime = DateTime.Now;

                if (ticket.BoughtTime.Year == dateTime.Year)
                {
                    result = "Valid ticket";
                }
                else
                {
                     result = "Year has expired. Invalid";
                }


            }

            return result;
        }

        // PUT: api/Tickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTicket(int id, Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.IDticket)
            {
                return BadRequest();
            }
            db.Tickets.Update(ticket);


            try
            {
                db.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        [Route("Buy")]
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult PostTicket(string TypeOfTicket, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticketId = db.TypesOfTicket.GetAll().Where(t => t.typeOfTicket == TypeOfTicket).FirstOrDefault();
            

            Ticket ticket = new Ticket();
            ticket.BoughtTime = DateTime.Now;
            ticket.UserName = UserName;
            ticket.IDtypeOfTicket = ticketId.IDtypeOfTicket;
            ticket.CheckIn = ticket.BoughtTime;

            db.Tickets.Add(ticket);
            db.Complete();
            return Ok();
        }

        // DELETE: api/Tickets/5
        [Route("CheckIn")]
        [HttpPut]
        public IHttpActionResult CheckInTicket([FromBody]Ticket t)
        {
            Ticket ticket = db.Tickets.Get(t.IDticket);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.CheckIn = DateTime.Now;
            db.Tickets.Update(ticket);
            
            db.Complete();

            return Ok(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(int id)
        {
            return db.Tickets.GetAll().Count(e => e.IDticket == id) > 0;
        }
    }
}