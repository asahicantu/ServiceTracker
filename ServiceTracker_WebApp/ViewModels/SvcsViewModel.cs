using System;
using System.Globalization;
using AutoMapper;
using ServiceTracker.DAL.Models;

namespace ServiceTracker.ViewModels
{
    public class ServiceViewModel : Service
    {
        private string _strDate = null;
        public string StrDate
        {
            get
            {
                if (string.IsNullOrEmpty(_strDate))
                {
                    _strDate = Date?.ToString("yyyy-MMM");
                }
                return _strDate;
            }
            set => _strDate = value;
        }
        public string Status { get; set; }

        public ServiceViewModel()
        {
            Status = "UNCHANGED";
        }

        internal void ParseData()
        {
            if (!string.IsNullOrEmpty(StrDate))
            {
                Date = DateTime.ParseExact(StrDate + "-01", "yyyy-MMM-dd", CultureInfo.CurrentCulture);
            }
        }

        internal void ParseAddedData()
        {
            Id = 0;
            ParseData();

        }
    }
}
