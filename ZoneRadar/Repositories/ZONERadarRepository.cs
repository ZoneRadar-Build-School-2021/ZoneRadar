using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Data;
using ZoneRadar.Models;

namespace ZoneRadar.Repositories
{
    public class ZoneRadarRepository
    {
        private readonly ZONERadarContext _ctx;
        public ZoneRadarRepository()
        {
            _ctx = new ZONERadarContext();
        }
        public IQueryable<T> GetAll<T>() where T : class
        {
            return _ctx.Set<T>();
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