using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModels;

namespace ZoneRadar.Services
{
    public class addressAreaService
    {
        private readonly FormAreaRepository _addressAreaRep;
        public addressAreaService() 
        {
            _addressAreaRep = new FormAreaRepository();
        }
        public FormAreaViewModel readFormAreaSelect()
        {
            var result = new FormAreaViewModel()
            {
                adreessList = new List<adreessCard>()
            };
            var districts = _addressAreaRep.GetAllDistricts().Select(x => x).ToList();
            foreach (var district in districts)
            {
                var districtTemp = new adreessCard
                {
                    DistricName = district.DistrictName
                };
                result.adreessList.Add(districtTemp);
            }
            List<SpaceType> SpaceTypeList = new List<SpaceType>()
            {

            };

            return result;
        }
    }
}