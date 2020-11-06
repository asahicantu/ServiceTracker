using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using ServiceTracker.DAL.Models;

namespace ServiceTracker.ViewModels
{
    public class CatalogViewModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public CatalogViewModel(string code, string name)
        {
            Id = code;
            Name = name;
        }
        public CatalogViewModel(int id, string name)
        {
            Id = id.ToString();
            Name = name;
        }
    }
    public class CatalogsViewModel 
    {
        public List<CatalogViewModel> Clients { get; set; }
        public List<CatalogViewModel> SDLS { get; set; }
        public List<CatalogViewModel> AMS { get; set; }
        public List<CatalogViewModel> Employees { get; set; }
        public List<CatalogViewModel> CostTypes { get; set; }
        public List<CatalogViewModel> Countries { get; set; }
        public List<CatalogViewModel> Currencies { get; set; }
        public List<CatalogViewModel> Portfolios { get; set; }
        public List<CatalogViewModel> Subportfolios { get; set; }
        public List<CatalogViewModel> Bls{ get; set; }


        public CatalogsViewModel()
        {

        }
    }


}
