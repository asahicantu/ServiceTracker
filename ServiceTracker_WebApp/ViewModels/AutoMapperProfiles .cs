using AutoMapper;
using ServiceTracker.DAL.Models;
using ServiceTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTracker.AutoMapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Service, ServiceViewModel>();
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
