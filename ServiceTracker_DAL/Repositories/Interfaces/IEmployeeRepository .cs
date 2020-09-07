using DAL.Repositories.Interfaces;
using ServiceTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTracker.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
