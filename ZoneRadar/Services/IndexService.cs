using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Repositories;
using ZoneRadar.ViewModels;

namespace ZoneRadar.Services
{
    public class IndexService
    {
        private readonly ZoneRadarRepository _repo;
        public IndexService()
        {
            _repo = new ZoneRadarRepository();
        }
        public List<SpaceVM> GetSpaceVMData()
        {
            List<Space> spaces = _repo.ReadSpaceData();
            List<City> cities = _repo.ReadCityData();

            List<SpaceVM> spaceVMList = new List<SpaceVM>();

            foreach (var item in spaces)
            {
                var cityName = cities.Where(x => x.CityID == item.CityID).Select(x => x.CityName).FirstOrDefault();

                spaceVMList.Add(new SpaceVM { City = cityName, Capacity = item.Capacity, PricePerHour = item.PricePerHour});
            }

            return spaceVMList;
        }
    }
    
}