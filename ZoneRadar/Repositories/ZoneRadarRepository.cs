using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;

namespace ZoneRadar.Repositories
{ 

    public class ZoneRadarRepository
    {
        private readonly ZoneRadarDataContext _ctx;
        public ZoneRadarRepository()
        {
            _ctx = new ZoneRadarDataContext();
        }
        public List<Space> ReadSpaceData()
        {
            return _ctx.Space.ToList();
        }
        public List<City> ReadCityData()
        {
            return _ctx.City.ToList();
        }
    }
}