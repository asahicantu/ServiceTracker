// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceTracker.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTracker.DAL
{
    public interface IUnitOfWork
    {

        IServiceRepository Service { get; }
        IEmployeeRepository Employee { get; }
        ICompanyRepository Company { get; }
        ICountryRepository Country { get; }
        ICostTypeRepository CostType { get; }
        ICurrencyRepository Currency { get; }
        IPortfolioRepository Portfolio { get; }
        ISubportfolioRepository Subportfolio { get; }
        int SaveChanges();
    }
}
