using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModel;


namespace ZoneRadar.Service
{
    public class AmenityService
    {
        private readonly FormAreaRepository _formRepository;
        public AmenityService()
        {
            _formRepository = new FormAreaRepository();
        }
       
    }
}