using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Repositories;
using ZoneRadar.ViewModels;
using ZoneRadar.Models;

namespace ZoneRadar.Services
{
    public class SpaceService
    {
        private readonly ZoneRadarRepository _repo;
        public SpaceService() 
        {
            _repo = new ZoneRadarRepository();
        }
        public List<SpaceVM> GetSpaceVM()
        {
            var spaces=  _repo.ReadSpaceData();
            List<City> cities = _repo.ReadCityData();
            List<SpaceVM> spaceVMsList = new List<SpaceVM>();
            foreach(var item in spaces)
            {
                var cityName = cities.Where(x => x.CityID == item.CityID).Select(x=>x.CityName).FirstOrDefault();
                spaceVMsList.Add(new SpaceVM { City = cityName });
            }
            return spaceVMsList;
        }
        
    }
}