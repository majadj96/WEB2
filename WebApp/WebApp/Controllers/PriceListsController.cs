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
    [RoutePrefix("api/PriceList")]
    public class PriceListsController : ApiController
    {
       // private ApplicationDbContext db = new ApplicationDbContext();

        private IUnitOfWork db;
        public PriceListsController(IUnitOfWork db)
        {
            this.db = db;
        }
        [Authorize(Roles = "Admin")]
        [Route("GetPriceListAdmin")]
        // GET: api/PriceList/GetPriceListAdmin
        public IEnumerable<PriceListLine> GetPriceListAdmin()
        {
            List<PriceList> priceLists = db.PriceLists.GetAll().ToList();
            List<PriceListLine> ret = new List<PriceListLine>();

            foreach(var v in priceLists)
            {
                foreach(var price in v.Prices)
                {
                    PriceListLine p = new PriceListLine();
                    p.ValidFrom = v.ValidFrom;
                    p.TypeOfTicket = db.TypesOfTicket.GetAll().FirstOrDefault(u => u.IDtypeOfTicket == price.IDtypeOfTicket).typeOfTicket;
                    p.Value = price.Value;
                    p.IDPrice = price.IDPrice;
                    p.IDPriceList = v.IDPriceList;
                    ret.Add(p);

                }
               
            }



            return ret.OrderBy(o => o.ValidFrom).ToList(); ;
        }

        // GET: api/PriceLists/5
        [ResponseType(typeof(PriceList))]
        public IHttpActionResult GetPriceList(int id)
        {
            PriceList priceList = db.PriceLists.Get(id);
            if (priceList == null)
            {
                return NotFound();
            }

            return Ok(priceList);
        }

        // PUT: api/PriceLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceList(int id, PriceList priceList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceList.IDPriceList)
            {
                return BadRequest();
            }

            db.PriceLists.Update(priceList);

            try
            {
                db.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceListExists(id))
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
        [Route("PostPriceListLine")]
        // POST: api/PriceList/addPriceListLine
        [ResponseType(typeof(PriceList))]
        public string PostPriceListLine(PriceListLine priceListLine)
        {
            if (!ModelState.IsValid)
            {
                return "bad";
            }
            if (priceListLine == null)
            {
                return "null";
            }

            PriceList priceListExist = db.PriceLists.GetAll().FirstOrDefault(u => u.ValidFrom == priceListLine.ValidFrom);
            int id = db.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == priceListLine.TypeOfTicket).IDtypeOfTicket;
            Price priceExist = db.Prices.GetAll().FirstOrDefault(u => (u.Value == priceListLine.Value && u.IDtypeOfTicket == id));
            Price newPrice = new Price();
            if(priceExist == null)
            {

                newPrice.PriceLists = new List<PriceList>();
                newPrice.Value = priceListLine.Value;
                newPrice.IDtypeOfTicket = db.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == priceListLine.TypeOfTicket).IDtypeOfTicket;
            }

            if(priceListExist == null)
            {
                
             /*   int day = priceListLine.ValidFrom.Day;
                int m = priceListLine.ValidFrom.Month;
                int y = priceListLine.ValidFrom.Year;
                DateTime dt = new DateTime(day,m,y);*/
                PriceList newPriceList = new PriceList() { ValidFrom = priceListLine.ValidFrom, ValidTo= priceListLine.ValidFrom};
                newPriceList.Prices = new List<Price>();
                if (priceExist == null)
                {
                    try
                    {
                        newPriceList.Prices.Add(newPrice);
                        db.PriceLists.Add(newPriceList);
                        newPrice.PriceLists.Add(newPriceList);
                        db.Prices.Add(newPrice);
                        db.Complete();
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    newPriceList.Prices.Add(priceExist);
                    db.PriceLists.Add(newPriceList);
                    priceExist.PriceLists.Add(newPriceList);
                    db.Prices.Update(priceExist);
                }
            }
            else
            {
              // int idType = db.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == priceListLine.TypeOfTicket).IDtypeOfTicket;
                foreach(Price p in priceListExist.Prices)
                {
                    if(p.IDtypeOfTicket == id)
                    {
                        return "type of ticket for this price list exists!";
                    }
                }

                if (priceExist == null)
                {
                    priceListExist.Prices.Add(newPrice);
                    db.PriceLists.Update(priceListExist);
                    newPrice.PriceLists.Add(priceListExist);
                    db.Prices.Add(newPrice);
                }
                else
                {
                    priceListExist.Prices.Add(priceExist);
                    db.PriceLists.Update(priceListExist);
                    priceExist.PriceLists.Add(priceListExist);
                    db.Prices.Update(priceExist);
                }
              

            }

            try
            {
                db.Complete();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "ok";
        }
        [Authorize(Roles = "Admin")]
        [Route("EditLine")]
        // POST: api/PriceList/addPriceListLine
        [ResponseType(typeof(PriceList))]
        public string EditPriceListLine(PriceListLine priceListLine)
        {
            if (!ModelState.IsValid)
            {
                return "bad";
            }
            if(priceListLine == null)
            {
                return "null";
            }

            PriceList priceListExist = db.PriceLists.GetAll().FirstOrDefault(u => u.ValidFrom == priceListLine.ValidFrom);
            int id = db.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == priceListLine.TypeOfTicket).IDtypeOfTicket;
            Price priceExist = db.Prices.GetAll().FirstOrDefault(u => (u.Value == priceListLine.Value && u.IDtypeOfTicket == id));
            Price newPrice = new Price();
            if (priceExist == null)
            {

                newPrice.PriceLists = new List<PriceList>();
                newPrice.Value = priceListLine.Value;
                newPrice.IDtypeOfTicket = db.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == priceListLine.TypeOfTicket).IDtypeOfTicket;
            }

            Price priceFromBase = db.Prices.GetAll().FirstOrDefault(u => u.IDPrice == priceListLine.IDPrice);

            if (priceFromBase.PriceLists.Count == 1)
            {
                PriceList exist = db.PriceLists.GetAll().FirstOrDefault(u => (u.ValidFrom.Day == priceListLine.ValidFrom.Day && u.ValidFrom.Month == priceListLine.ValidFrom.Month && u.ValidFrom.Year == priceListLine.ValidFrom.Year));
                priceFromBase.Value = priceListLine.Value;
                db.Prices.Update(priceFromBase);
                    
              
              
                for (int i = 0; i < exist.Prices.Count; i++)
                {
                    if (exist.Prices[i].IDPrice == priceFromBase.IDPrice)
                    {
                        exist.Prices[i] = priceFromBase;
                    }
                }

                    db.PriceLists.Update(exist);
               

            }
            else if (priceFromBase.PriceLists.Count > 1)
            {
                PriceList exist = db.PriceLists.GetAll().FirstOrDefault(u => (u.ValidFrom.Day == priceListLine.ValidFrom.Day && u.ValidFrom.Month == priceListLine.ValidFrom.Month && u.ValidFrom.Year == priceListLine.ValidFrom.Year));

                priceFromBase.PriceLists.Remove(exist);
                    exist.Prices.Remove(priceFromBase);
                    exist.Prices.Add(newPrice);
                    db.PriceLists.Update(exist);
                    newPrice.PriceLists.Add(exist);
                    db.Prices.Add(newPrice);
                }
            
           
            try
            {
                db.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "ok";
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteLine/{IDPriceList}/{IDPrice}")]
        // DELETE: api/PriceLists/5
        [ResponseType(typeof(PriceList))]
        public IHttpActionResult DeletePriceList(int IDPriceList,int IDPrice)
        {
            PriceList priceList = db.PriceLists.GetAll().FirstOrDefault(u => u.IDPriceList == IDPriceList);
            if (priceList == null)
            {
                return NotFound();
            }

            Price price = db.Prices.GetAll().FirstOrDefault(u => u.IDPrice == IDPrice);
            priceList.Prices.Remove(price);

            db.PriceLists.Update(priceList);
            db.Complete();

            return Ok(priceList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceListExists(int id)
        {
            return db.PriceLists.GetAll().Count(e => e.IDPriceList == id) > 0;
        }
    }
}