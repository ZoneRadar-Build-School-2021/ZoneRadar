using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JSONAPIController : ApiController
    {
        private readonly SpaceService _spaceService;
        private readonly ZONERadarRepository _repository;
        public JSONAPIController()
        {
            _spaceService = new SpaceService();
            _repository = new ZONERadarRepository();
        }

        [AcceptVerbs("GET")]
        public IHttpActionResult GetFilterData()
        {
            var citiesAndDistricts = _repository.GetAll<District>().GroupBy(x => x.City).OrderBy(x => x.Key.CityID).ToDictionary(x => x.Key.CityName, x => x.Select(y => y.DistrictName).ToList());
            var spaceTypeList = _repository.GetAll<TypeDetail>().OrderBy(x => x.TypeDetailID).Select(x => x.Type).ToList();
            var amenityList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.Amenity).ToList();
            var amenityIconList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.AmenityICON).ToList();
            
            var result = new FilterViewModel
            {
                CityDistrictDictionary = citiesAndDistricts,
                SpaceTypeList = spaceTypeList,
                AmenityList = amenityList,
                AmenityIconList = amenityIconList,
                SelectedCity = "臺北市",
                SelectedDistrict = "",
                SelectedType = "運動",
                SelectedDate = ""
            };

            var json = JsonConvert.SerializeObject(result);

            return Ok(json);
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetFilteredSpaces(QueryViewModel query)
        {
            var city = query.City;
            var district = query.District;
            var type = query.Type;
            var Date = query.Date;
            var lowPrice = query.LowPrice;
            var highPrice = query.HighPrice;
            var amenities = query.AmenityList;

            var queriedSpaces = _repository.GetAll<Space>().Where(x => x.City.CityName == city).Select(x => new SpaceBriefViewModel 
            {
                SpaceID = x.SpaceID,
                SpaceName = x.SpaceName,
                SpaceImageURLList = x.SpacePhoto.Where(y => y.SpaceID == x.SpaceID).Select(y => y.SpacePhotoUrl).ToList(),
                Address = x.Address,
                Capacity = x.Capacity,
                PricePerHour = x.PricePerHour,
                Country = x.City.CityName,
                City = x.City.CityName,
                District = x.District.DistrictName,
                MinHour = x.MinHours,
                MeasurementOfArea = x.MeasureOfArea,
            }).ToList();
            
            var json = JsonConvert.SerializeObject(queriedSpaces);

            return Ok(json);
        }
    }
}
