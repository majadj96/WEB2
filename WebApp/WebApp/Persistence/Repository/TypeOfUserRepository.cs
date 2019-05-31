using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class TypeOfUserRepository : Repository<TypeOfUser, int>, ITypeOfUserRepository
    {
        public TypeOfUserRepository(DbContext context) : base(context) { }

    }
}