// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTracker.DAL
{
    public interface IUnitOfWork
    {

        IRepository<Service> Service { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Company> Company { get; }
        IRepository<BL> BL { get; }
        IRepository<Country> Country { get; }
        IRepository<CostType> CostType { get; }
        IRepository<Currency> Currency { get; }
        IRepository<Portfolio> Portfolio { get; }
        IRepository<SubPortfolio> Subportfolio { get; }
        int SaveChanges();
    }
}
