using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [RoutePrefix("api/Price")]
    public class PriceController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        private DbContext _db;

        public PriceController(IUnitOfWork unitOfWork, DbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public Price getLatestPrice(string ticket)
        {
            List<PriceList> priceLists = _unitOfWork.PriceLists.GetAll().OrderBy(u => u.ValidFrom).ToList();
            int idType = _unitOfWork.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == ticket).IDtypeOfTicket;
            foreach (PriceList pl in priceLists)
            {
                foreach(Price p in pl.Prices)
                {
                    if (p.IDtypeOfTicket == idType)
                    {
                        return p;
                    }
                }
            }

            return null;
        }

      //  [HttpGet]
        [Route("GetOnePrice")]
        public double GetOnePrice(string ticket, string user)
        {

            var userr = _unitOfWork.TypesOfUser.GetAll().FirstOrDefault(u => u.typeOfUser == user);
            double pretenge =1; //popust
            double popust = (double)userr.Percentage;
            

            var tickett = _unitOfWork.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == ticket);

            Price pricee = getLatestPrice(ticket);// _unitOfWork.Prices.GetAll().FirstOrDefault(u => u.IDtypeOfTicket == tickett.IDtypeOfTicket);
            if (pricee == null)
                return 0;

            if (userr.Percentage != 0)
                pretenge = popust / 100;


            return pricee.Value*pretenge; //popust


        }


        [Route("GetPrice")]
        public double GetPrice(string ticket, string email)
        {

            double pretenge = 1; //popust


            var email1 = Request.GetOwinContext().Authentication.User.Identity.Name;
            ApplicationUserManager cont = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            List<ApplicationUser> app = cont.Users.ToList();

            ApplicationUser apUs = app.Where(u => u.Email == email).FirstOrDefault();

            var tickett = _unitOfWork.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == ticket); //koja karta

            var pricee = getLatestPrice(ticket);// _unitOfWork.Prices.GetAll().FirstOrDefault(u => u.IDtypeOfTicket == tickett.IDtypeOfTicket);//koliko kosta
            var userr = _unitOfWork.TypesOfUser.GetAll().FirstOrDefault(u => u.IDtypeOfUser == apUs.IDtypeOfUser);
            double popust = (double)userr.Percentage;

            pretenge = popust / 100;




            return pricee.Value * pretenge;
        }

            //// GET: api/Price
            //public IEnumerable<Price> Get()
            //{

            //    return _unitOfWork.Prices.GetAll();
            //}

            //// GET: api/Price/5
            //public string Get(int id)
            //{
            //    return "value";
            //}

            //// POST: api/Price
            //public void Post([FromBody]string value)
            //{
            //}

            //// PUT: api/Price/5
            //public void Put(int id, [FromBody]string value)
            //{
            //}

            //// DELETE: api/Price/5
            //public void Delete(int id)
            //{
            //}
        }
}
