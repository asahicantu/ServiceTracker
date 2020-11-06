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
using ServiceTracker.DAL.Repositories;
using System.Runtime.InteropServices;

namespace ServiceTracker.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly STContext _context;

        private IRepository<Company>_company;
        private IRepository<Employee>_employee;
        private IRepository<Service>_service;
        private IRepository<Country>_country;
        private IRepository<CostType>_costType;
        private IRepository<Currency>_currency;
        private IRepository<Portfolio>_portfolio;
        private IRepository<SubPortfolio>_subportfolio;
        private IRepository<BL>_bl;


        public UnitOfWork(STContext context)
        {
            _context = context;
        }

        public IRepository<Portfolio> Portfolio
        {
            get
            {
                if (_portfolio == null)
                {
                    _portfolio = new PortfolioRepository(_context);
                }
                return _portfolio;
            }

        }

        public IRepository<SubPortfolio> Subportfolio
        {
            get
            {
                if (_subportfolio == null)
                    _subportfolio = new SubportfolioRepository(_context);
                return _subportfolio;
            }
        }


        public IRepository<Company>  Company
        {
            get
            {
                if (_company == null)
                    _company = new CompanyRepository(_context);
                return _company;
            }
        }

        public IRepository<Employee> Employee
        {
            get
            {
                if (_employee == null)
                    _employee = new EmployeeRepository(_context);
                return _employee;
            }
        }

        public IRepository<Service> Service
        {
            get
            {
                if (_service == null)
                    _service = new ServiceRepository(_context);
                return _service;
            }
        }

        public IRepository<Country> Country
        {
            get
            {
                if (_country == null)
                    _country = new CountryRepository(_context);
                return _country;
            }
        }

        public IRepository<CostType> CostType
        {
            get
            {
                if (_costType == null)
                    _costType = new CostTypeRepository(_context);
                return _costType;
            }
        }

        public IRepository<Currency> Currency
        {
            get
            {
                if (_currency == null)
                    _currency = new CurrencyRepository(_context);
                return _currency;
            }
        }

        public IRepository<BL> BL
        {
            get
            {
                if (_bl == null)
                    _bl = new BLRepository(_context);
                return _bl;

            }
        }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
