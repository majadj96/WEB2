using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
    
        [Dependency]
        public IDepartureRepository Departures { get; set; }
        [Dependency]
        public ILineRepository Lines { get; set; }
        [Dependency]
        public IStationRepository Stations { get; set; }
        [Dependency]
        public ITicketRepository Tickets { get; set; }
        [Dependency]
        public IVehicleRepository Vehicles { get; set; }

        [Dependency]
        public ITypeOfLineRepository TypesOfLine { get; set; }
        [Dependency]
        public ITypeOfTicketRepository TypesOfTicket { get; set; }
        [Dependency]
        public ITypeOfUserRepository TypesOfUser { get; set; }
        [Dependency]
        public IDayRepository Days { get; set; }

        [Dependency]
        public IPriceRepository Prices { get; set; }

        [Dependency]
        public IPriceListRepository PriceLists { get; set; }

        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}