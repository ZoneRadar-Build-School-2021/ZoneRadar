using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;

namespace ZoneRadar.Repositories
{
    /// <summary>
    /// 新增功能中下拉選項的縣市 行政區 
    /// </summary>
    public class FormAreaRepository
    {
        private readonly ZoneRadarDB _zoneradarDB;
        public  FormAreaRepository()
        {
            _zoneradarDB = new ZoneRadarDB();
        }
        public IQueryable<City> GetAllCities()
        {
            var cities = _zoneradarDB.City;
            return cities;
        }
        public IQueryable<District> GetAllDistricts()
        {
            var districts = _zoneradarDB.District;
            return districts;
        }
        public IQueryable<AmenityDetail> GetAllAmenities()
        {
            var Amenities = _zoneradarDB.AmenityDetail;
            return Amenities;
        }
    }
}