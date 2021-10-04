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

        /// <summary>
        /// 找出精選場地
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 產生活動類型的SelectListItem
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetTypeOptions()
        {
            var types = _repository.GetAll<TypeDetail>().ToList();
            var typeOptions = types.Select(x => new SelectListItem
            {
                Value = x.TypeDetailID.ToString(),
                Text = x.Type
            }).ToList();
            
            return typeOptions;
        }

        /// <summary>
        /// 產生城市的SelectListItem
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetCityOptions()
        {
            var cities = _repository.GetAll<City>().ToList();
            var cityOptions = cities.Select(x => new SelectListItem
            {
                Value = x.CityID.ToString(),
                Text = x.CityName
            }).ToList();

            return cityOptions;
        }

        /// <summary>
        /// 關閉資料庫連線
        /// </summary>
        public void DisposeCtx()
        {
            _repository.Dispose();
        }

        /// <summary>
        /// 搜尋符合類型、縣市、時間條件的場地(首頁搜尋列)，並轉成搜尋場地頁面的ViewModel
        /// </summary>
        /// <param name="homepageSearchVM"></param>
        public void SearchSpacesByTypeCityDate(HomepageSearchViewModel homepageSearchVM)
        {
            var orders = _repository.GetAll<Order>().ToList();
            var targetSpaces = _repository.GetAll<Space>().ToList();
            var operatings = _repository.GetAll<Operating>().ToList();
            var orderDetails = _repository.GetAll<OrderDetail>().ToList();
            var spaceTypes = _repository.GetAll<SpaceType>().ToList();

            //1. 找到符合「城市」條件的場地
            if (homepageSearchVM.CityId != 0)
            {
                targetSpaces = targetSpaces.Where(x => x.City.CityID == homepageSearchVM.CityId).ToList();
            }

            //2. 找到符合「類型」條件的場地
            if (homepageSearchVM.TypeDetailId != 0 && targetSpaces.Count() != 0)
            {
                //(此方法搜尋的順序不太對)
                //targetSpaces = spaceTypes.Where(x => x.TypeID == homepageSearchVM.TypeDetailId).Select(x => targetSpaces.FirstOrDefault(y => y.SpaceID == x.SpaceID)).ToList();

                foreach (var space in targetSpaces)
                {
                    var hasType = spaceTypes.Where(x => x.SpaceID == space.SpaceID).Select(x => x.TypeDetailID).Any(x => x == homepageSearchVM.TypeDetailId);
                    if (!hasType)
                    {
                        targetSpaces.Remove(space);
                    }
                }
            }
           
            //3. 找到符合「時間」條件的場地
            if(homepageSearchVM.Date != new DateTime() && targetSpaces.Count() != 0)
            {
                foreach (var space in targetSpaces)
                {
                    //找到該場地的未完成訂單
                    var unfinishedOrders = orders.Where(x => x.SpaceID == space.SpaceID && x.OrderStatus.OrderStatusID == 2);
                    //找到該場地的未完成訂單被訂走的日期
                    var bookedDate = new List<DateTime>();
                    foreach (var order in unfinishedOrders)
                    {
                        bookedDate.AddRange(orderDetails.Where(x => x.OrderID == order.OrderID).Select(x => x.StartDateTime.Date).ToList());
                    }
                    //找到該場地的營業星期
                    var operatingWeekDay = operatings.Where(x => x.SpaceID == space.SpaceID).Select(x => x.OperatingDay).ToList();
                    //將營業星期陣列中的7改成0(為了符合DayOfWeek列舉)
                    if (operatingWeekDay.Remove(7))
                    {
                        operatingWeekDay.Add(0);
                    }
                    //判斷該天是否被訂走或未營業
                    var isBooked = bookedDate.Contains(homepageSearchVM.Date.Date); //該天被訂走
                    var isOperating = operatingWeekDay.Contains((int)homepageSearchVM.Date.DayOfWeek); //該天有營業
                                                                                                                     //若被訂走或未營業，則將其從targetSpaces中移除
                    if (isBooked && !isOperating)
                    {
                        targetSpaces.Remove(space);
                    }
                }
            }

            //如果targetSpaces.Count()到這裡等於0，代表沒找到符合條件的場地，要顯示找不到頁面

            //4. 將符合所有條件的場地轉成ViewModel
            foreach (var space in targetSpaces)
            {

            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 依照CityId搜尋場地，並轉成搜尋場地頁面的ViewModel
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public void SearchSpacesByCity(int cityId)
        {
            var spaces = _repository.GetAll<Space>().ToList();
            var spacesByCity = new List<Space>();
            //若選擇新竹，則搜尋新竹市和新竹縣
            if (cityId == 5 || cityId == 10)
            {
                spacesByCity = spaces.Where(x => x.CityID == 5 && x.CityID == 10).ToList();
            }
            //若選擇嘉義，則搜尋嘉義市和嘉義縣
            else if (cityId == 8 || cityId == 15)
            {
                spacesByCity = spaces.Where(x => x.CityID == 8 && x.CityID == 15).ToList();
            }
            else
            {
                spacesByCity = spaces.Where(x => x.CityID == cityId).ToList();
            }
            //轉換成ViewModel
            foreach (var item in spacesByCity)
            {

            }
            throw new NotImplementedException();
        }
    }
}