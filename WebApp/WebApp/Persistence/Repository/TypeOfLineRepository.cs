using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class TypeOfLineRepository : Repository<TypeOfLine, int>, ITypeOfLineRepository
    {
        public TypeOfLineRepository(DbContext context) : base(context) { }

    }
}