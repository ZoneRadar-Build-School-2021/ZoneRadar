using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class SpaceService
    {
        private readonly ZONERadarRepository _repository;
        public SpaceService()
        {
            _repository = new ZONERadarRepository();
        }
        public List<SelectedSpaceViewModel> GetSelectedSpace()
        {
            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().Where(x => x.OrderStatus.OrderStatusID == 2).ToList();
            var reviews = _repository.GetAll<Review>().Where(x => x.ToHost).ToList();
            var spacePhotos = _repository.GetAll<SpacePhoto>().ToList();

            var selectedSpaces = new List<SelectedSpaceViewModel>();

            foreach (var item in spaces)
            {
                //計算場地平均分數
                var spaceReview = orders.Where(x => x.SpaceID == item.SpaceID).Select(x => reviews.FirstOrDefault(y => y.OrderID == x.OrderID)).OfType<Review>().ToList();
                double scoreAvg = spaceReview.Count() == 0 ? 0 : spaceReview.Average(x => x.Score);

                //場地圖片資料表還沒建好，先寫防呆程式
                var spacePhoto = spacePhotos.FirstOrDefault(x => x.SpaceID == item.SpaceID);
                var spacePhotoUrl = spacePhoto == null ? "" : spacePhoto.SpacePhotoUrl;

                selectedSpaces.Add(
                    new SelectedSpaceViewModel
                    {
                        SpaceId = item.SpaceID,
                        CityName = item.City.CityName,
                        Capacity = item.Capacity,
                        PricePerHour = item.PricePerHour,
                        SpacePhoto = spacePhotoUrl,
                        Score = scoreAvg
                    });
            }

            var topSelectedSpaces = selectedSpaces.OrderByDescending(x => x.Score).Take(6).ToList();

            return topSelectedSpaces;
        }
        public List<SelectListItem> GetTypeOption()
        {
            var typeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "會議", Text = "會議" },
                new SelectListItem { Value = "派對", Text = "派對" },
                new SelectListItem { Value = "聚會", Text = "聚會" },
                new SelectListItem { Value = "私人談話", Text = "私人談話" },
                new SelectListItem { Value = "課程講座", Text = "課程講座" },
                new SelectListItem { Value = "運動", Text = "運動" },
                new SelectListItem { Value = "工作", Text = "工作" },
                new SelectListItem { Value = "展覽", Text = "展覽" },
                new SelectListItem { Value = "音樂/表演", Text = "音樂/表演" },
                new SelectListItem { Value = "婚禮", Text = "婚禮" },
                new SelectListItem { Value = "拍照/攝影", Text = "拍照/攝影" },
                new SelectListItem { Value = "美容", Text = "美容" },
                new SelectListItem { Value = "烹飪", Text = "烹飪" },
                new SelectListItem { Value = "儲物", Text = "儲物" },
                new SelectListItem { Value = "發表會", Text = "發表會" },
                new SelectListItem { Value = "親子活動", Text = "親子活動" },
                new SelectListItem { Value = "其他", Text = "其他" }
            };

            return typeOptions;
        }
        public List<SelectListItem> GetCityOption()
        {
            var cities = _repository.GetAll<City>().ToList();
            var cityOptions = cities.Select(x => new SelectListItem
            {
                Value = x.CityID.ToString(),
                Text = x.CityName
            }).ToList();

            return cityOptions;
        }
    }
}