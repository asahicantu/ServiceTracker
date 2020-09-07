using DAL.Repositories.Interfaces;
using ServiceTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTracker.DAL.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
    }
}
