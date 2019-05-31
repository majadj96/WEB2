using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class DayRepository : Repository<Day, int>, IDayRepository
    {
        public DayRepository(DbContext context) : base(context) { }

    }
}