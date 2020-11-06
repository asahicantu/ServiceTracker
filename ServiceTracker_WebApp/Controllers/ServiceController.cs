using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceTracker.DAL;
using ServiceTracker.DAL.Models;
using ServiceTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ServiceTracker.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ServiceController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        [HttpGet("[action]")]
        public IActionResult GetCatalogs()
        {
            var sdls = _unitOfWork.Employee.Entities.Where(e => e.IsServiceDeliveryManager == true).Select(c => new CatalogViewModel(c.Id, c.Alias)).ToList();
            sdls.Add(new CatalogViewModel(-1, "ALL"));
            var catalogs = new CatalogsViewModel
            {
                SDLS = sdls,
                AMS = _unitOfWork.Employee.Entities.Where(e => e.IsAccountManager == true).Select(c => new CatalogViewModel(c.Id, c.Alias)).ToList(),
                Clients = _unitOfWork.Company.Entities.Select(c => new CatalogViewModel(c.Id, c.Name)).ToList(),
                Bls = _unitOfWork.BL.Entities.Select(c => new CatalogViewModel(c.Id, c.Name)).ToList(),
                Countries = _unitOfWork.Country.Entities.Select(c => new CatalogViewModel(c.Code, c.Code)).ToList(),
                Currencies = _unitOfWork.Currency.Entities.Select(c => new CatalogViewModel(c.Code, c.Code)).ToList(),
                Employees = _unitOfWork.Employee.Entities.Where(e => e.IsEngineer == true).Select(e=> new CatalogViewModel(e.Id,e.FullName1)).ToList(),
                CostTypes = _unitOfWork.CostType.Entities.Select(c => new CatalogViewModel(c.Id, c.Name)).ToList(),
                Portfolios =  _unitOfWork.Portfolio.Entities.Select(c => new CatalogViewModel(c.Id,c.Name)).ToList(),
                Subportfolios = _unitOfWork.Subportfolio.Entities.Select(s => new CatalogViewModel(s.Id, s.Name)).ToList(),
                

            };
            return Ok(catalogs);
        }

        [HttpGet("[action]")]
        public IActionResult GetSdls()
        {
            var sdls = _unitOfWork.Employee.Entities.Where(e => e.IsServiceDeliveryManager == true).Select(e => _mapper.Map<Employee, EmployeeViewModel>(e));
            return Ok(sdls);
        }

        [HttpGet("[action]")]
        public IActionResult GetCompanies()
        {
            var commpanies = _unitOfWork.Company.Entities.Select(c => c.Name);
            return Ok(commpanies);
        }



        [HttpGet("[action]")]
        public IActionResult GetData(string sdl, string from, string to)
        {
            var dtFrom = DateTime.ParseExact(from, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentCulture);
            var dtTo = DateTime.ParseExact(to, "dd-MM-yyyy", System.Globalization.CultureInfo.CurrentCulture);
            var svcs = new List<ServiceViewModel>();
            if(sdl == "ALL")
            {
                svcs = _unitOfWork.Service.Entities.Where(s=> s.Date >= dtFrom && s.Date <= dtTo).Select(s => _mapper.Map<Service, ServiceViewModel>(s)).ToList();
            }
            else
            {
                svcs = _unitOfWork.Service.Entities.Where(s => s.Sdl == sdl && s.Date >= dtFrom && s.Date <= dtTo).Select(s => _mapper.Map<Service, ServiceViewModel>(s)).ToList();
            }
            
            //svcs.Add(new ServiceViewModel() { Status = "SUMMARY" });
            return Ok(svcs);
        }


        [HttpPut("[action]")]
        public IActionResult Commit([FromBody] IEnumerable<ServiceViewModel> svcs, string sdl, string from, string to)
        {
            if (svcs != null)
            {
                var delSvcs = svcs.Where(s => s.Status == "DELETED").ToList();
                var addsVcs = svcs.Where(s => s.Status == "ADDED").ToList();
                var updSvcs = svcs.Where(s => s.Status == "UPDATED").ToList();
                addsVcs.ForEach(s => s.ParseAddedData());
                updSvcs.ForEach(s => s.ParseData());
                _unitOfWork.Service.RemoveRange(delSvcs);
                _unitOfWork.Service.AddRange(addsVcs);
                _unitOfWork.Service.UpdateRange(updSvcs);
                var saveResult = _unitOfWork.SaveChanges();
            }
            return GetData(sdl, from, to);
        }

        [HttpPut("[action]")]
        public IActionResult Block([FromBody] IEnumerable<ServiceViewModel> svcs)
        {
            ChangeBlockStatus(svcs, 1);
            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult Unblock([FromBody] IEnumerable<ServiceViewModel> svcs)
        {
            ChangeBlockStatus(svcs, 0);
            return Ok();
        }

        private void ChangeBlockStatus(IEnumerable<ServiceViewModel> svcs, int blocked)
        {
            var ids = svcs.Select(s => s.Id);

            var inId = new SqlParameter("@inId", string.Join(',', ids));
            _unitOfWork.Service.Entities.FromSqlRaw($"UPDATE SERVICE SET Blocked = 1 where Id in (@inId)", inId);
        }

        private void DeleteData(IEnumerable<ServiceViewModel> svcs)
        {
            var ids = svcs.Select(s => s.Id);
            var inId = new SqlParameter("@inId", string.Join(',', ids));
            _unitOfWork.Service.Entities.FromSqlRaw($"DELETE SERVICE WHERE Id IN (@inId)", inId);
        }



    }
}
