﻿// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceTracker.DAL.Models;
using ServiceTracker.DAL.Repositories.Interfaces;

namespace ServiceTracker.DAL.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(STContext context) : base(context)
        { }


        private STContext _appContext => _context;
    }
}
