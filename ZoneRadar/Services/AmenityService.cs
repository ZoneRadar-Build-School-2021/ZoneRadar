using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModels;


namespace ZoneRadar.Services
{
    public class AmenityService
    {
        private readonly FormAreaRepository _formRepository;
        public AmenityService()
        {
            _formRepository = new FormAreaRepository();
        }
        public FormAreaViewModel ShowSelectofCheckBox() 
        {
            var result = new FormAreaViewModel()
            {
                AmenityList=new List<AmenityCard>()
            };
            var amenties=_formRepository.GetAllAmenities().Select(x=>x).ToList();
            foreach (var amenty in amenties) 
            {
                var amentiesTemp = new AmenityCard()
                {
                    Amenity=amenty.Amenity,
                    AmenityDetailID=amenty.AmenityDetailID
                };
                result.AmenityList.Add(amentiesTemp);
            }
            return result;
        }

    }
}