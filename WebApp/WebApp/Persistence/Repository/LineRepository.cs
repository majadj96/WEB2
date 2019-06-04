using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class LineRepository : Repository<Line, string>, ILineRepository
    {
        public LineRepository(DbContext context) : base(context) { }

        
        public override IEnumerable<Line>GetAll()
        {
            return context.Set<Line>().Include(l => l.Departures);
        }
    }
}