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
    public class PricesController : ApiController
    {
        // private ApplicationDbContext db = new ApplicationDbContext();

        private IUnitOfWork db;
        public PricesController(IUnitOfWork db)
        {
            this.db = db;
        }

        // GET: api/Prices
        public IEnumerable<Price> GetPrices()
        {
            return db.Prices.GetAll();
        }

        // GET: api/Prices/5
        [ResponseType(typeof(Price))]
        public IHttpActionResult GetPrice(int id)
        {
            Price price = db.Prices.Get(id);
            if (price == null)
            {
                return NotFound();
            }

            return Ok(price);
        }


        // PUT: api/Prices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrice(int id, Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != price.IDPrice)
            {
                return BadRequest();
            }

            db.Prices.Update(price);

            try
            {
                db.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceExists(id))
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

        // POST: api/Prices
        [ResponseType(typeof(Price))]
        public IHttpActionResult PostPrice(Price price)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prices.Add(price);
            db.Complete();

            return CreatedAtRoute("DefaultApi", new { id = price.IDPrice }, price);
        }

        // DELETE: api/Prices/5
        [ResponseType(typeof(Price))]
        public IHttpActionResult DeletePrice(int id)
        {
            Price price = db.Prices.Get(id);
            if (price == null)
            {
                return NotFound();
            }

            db.Prices.Remove(price);
            db.Complete();

            return Ok(price);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceExists(int id)
        {
            return db.Prices.GetAll().Count(e => e.IDPrice == id) > 0;
        }
    }
}