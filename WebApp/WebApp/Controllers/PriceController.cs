using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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

      //  [HttpGet]
        [Route("GetOnePrice")]
        public double GetOnePrice(string ticket, string user)
        {

            var userr = _unitOfWork.TypesOfUser.GetAll().FirstOrDefault(u => u.typeOfUser == user);
            double pretenge =1; //popust
            double popust = (double)userr.Percentage;
            

            var tickett = _unitOfWork.TypesOfTicket.GetAll().FirstOrDefault(u => u.typeOfTicket == ticket);

            var pricee = _unitOfWork.Prices.GetAll().FirstOrDefault(u => u.IDtypeOfTicket == tickett.IDtypeOfTicket);

            if (userr.Percentage != 0)
                pretenge = popust / 100;


            return pricee.Value*pretenge; //popust


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
