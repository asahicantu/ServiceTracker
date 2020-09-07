// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceTracker.DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceTracker.DAL.Repositories.Interfaces;
using ServiceTracker.DAL.Repositories;

namespace ServiceTracker.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly STContext _context;

        ICompanyRepository _company;
        IEmployeeRepository _employee;
        IServiceRepository _service;
        ICountryRepository _country;
        ICostTypeRepository _costType;
        ICurrencyRepository _currency;
        IPortfolioRepository _portfolio;
        ISubportfolioRepository _subportfolio;


        public UnitOfWork(STContext context)
        {
            _context = context;
        }

        public IPortfolioRepository Portfolio
        {
            get
            {
                if(_portfolio == null)
                {
                    _portfolio = new PortfolioRepository(_context);
                }
                return _portfolio;
            }

        }

        public ISubportfolioRepository  Subportfolio
        {
            get
            {
                if (_subportfolio == null)
                    _subportfolio = new SubportfolioRepository(_context);
                return _subportfolio;
            }
        }


        public ICompanyRepository Company
        {
            get
            {
                if (_company == null)
                    _company = new CompanyRepository(_context);
                return _company;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employee == null)
                    _employee = new EmployeeRepository(_context);
                return _employee;
            }
        }

        public  IServiceRepository Service
        {
            get
            {
                if (_service == null)
                    _service = new ServiceRepository(_context);
                return _service;
            }
        }

        public ICountryRepository Country 
            {
            get
            {
                if (_country == null)
                    _country = new CountryRepository(_context);
                return _country;
            }
        }

        public ICostTypeRepository CostType
        {
            get
            {
                if (_costType == null)
                    _costType = new CostTypeRepository(_context);
                return _costType;
            }
        }

        public ICurrencyRepository Currency
        {
            get
            {
                if (_currency == null)
                    _currency = new CurrencyRepository(_context);
                return _currency;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
