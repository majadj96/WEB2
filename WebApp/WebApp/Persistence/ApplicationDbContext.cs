using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        DbSet<TypeOfLine> typeOfLine { get; set; }
        DbSet<Line> line { get; set; }
        DbSet<Station> station { get; set; }

        DbSet<Vehicle> vehicle { get; set; }

        DbSet<Day> day { get; set; }


        DbSet<Departure> departure { get; set; }

        DbSet<TypeOfUser> typeOfUser { get; set; }
        DbSet<TypeOfTicket> typeOfTicket { get; set; }
        DbSet<Ticket> ticket { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApp.Models.Line> Lines { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.Departure> Departures { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.Day> Days { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.Station> Stations { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.Vehicle> Vehicles { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.Ticket> Tickets { get; set; }

        public System.Data.Entity.DbSet<WebApp.Models.TypeOfTicket> TypeOfTickets { get; set; }
    }
}