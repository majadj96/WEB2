using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartureRepository Departures { get; set; }
        ILineRepository Lines { get; set; }
        IStationRepository Stations { get; set; }
        ITicketRepository Tickets { get; set; }
        IVehicleRepository Vehicles { get; set; }

        ITypeOfLineRepository TypesOfLine { get; set; }
        ITypeOfTicketRepository TypesOfTicket { get; set; }
        ITypeOfUserRepository TypesOfUser { get; set; }
        IDayRepository Days { get; set; }

        int Complete();
    }
}
