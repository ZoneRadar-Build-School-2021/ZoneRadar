using Newtonsoft.Json;
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
        /// 根據ID找到指定場地
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Space GetSpaceByID(int? id)
        {
            var result = _repository.GetAll<Space>().SingleOrDefault(x => x.SpaceID == id);
            return result;
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
            if (homepageSearchVM.Date != new DateTime() && targetSpaces.Count() != 0)
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

        /// <summary>
        /// 找出場地簡短資訊(搜尋頁可用)
        /// </summary>
        /// <param name="targetSpace"></param>
        /// <returns></returns>
        public SpaceBriefViewModel GetTargetSpaceBriefInfo(Space targetSpace)
        {
            var spacePhotoList = _repository.GetAll<SpacePhoto>().Where(x => x.SpaceID == targetSpace.SpaceID).OrderBy(x => x.Sort).Select(x => x.SpacePhotoUrl).ToList();
            var result = new SpaceBriefViewModel
            {
                SpaceID = targetSpace.SpaceID,
                PricePerHour = targetSpace.PricePerHour,
                SpaceImageURLList = spacePhotoList,
                SpaceName = targetSpace.SpaceName,
                Country = targetSpace.Country.CountryName,
                City = targetSpace.City.CityName,
                District = targetSpace.District.DistrictName,
                Address = targetSpace.Address,
                Capacity = targetSpace.Capacity,
                MinHour = targetSpace.MinHours,
                MeasurementOfArea = targetSpace.MeasureOfArea,
            };

            return result;
        }

        /// <summary>
        /// 找出場地詳細資訊
        /// </summary>
        /// <param name="targetSpace"></param>
        /// <returns></returns>
        public SpaceDetailViewModel GetTargetSpaceDetail(Space targetSpace)
        {
            // 找出所有場地設施
            var amenityList = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.AmenityDetail)
                              .GroupBy(x => x.AmenityCategoryDetail.AmenityCategory)
                              .ToDictionary(x => x.Key, x => x.Select(y => y.Amenity).ToList());
            var a = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.AmenityDetail)
                              .GroupBy(x => x.AmenityCategoryDetail.AmenityCategory)
                              .ToDictionary(x => x.Key, x => x.Select(y => y.AmenityICON).ToList());

            // 找出該場地所有營業資料
            var weekDayConverter = new Dictionary<string, int>
            {
                { "星期一", 1 },
                { "星期二", 2 },
                { "星期三", 3 },
                { "星期四", 4 },
                { "星期五", 5 },
                { "星期六", 6 },
                { "星期日", 7 }
            };
            var operationingList = _repository.GetAll<Operating>().Where(x => x.SpaceID == targetSpace.SpaceID);
            var operationgDayList = operationingList.Select(x => weekDayConverter.FirstOrDefault(y => y.Value == x.OperatingDay).Key);
            var startTimeList = operationingList.Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.StartTime);
            var endTimeList = operationingList.Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.EndTime);

            // 找出所有清潔公約選項
            var cleaningOptionList = _repository.GetAll<CleaningProtocol>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.CleaningOption)
                                     .GroupBy(x => x.CleaningCategory.Category)
                                     .ToDictionary(x => x.Key, x => x.Select(y => y.OptionDetail).ToList());

            // 找出滿時優惠的時數
            var hoursForDiscount = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID).Hour;

            // 找出滿時優惠的折數
            var discount = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID).Discount;

            var result = new SpaceDetailViewModel
            {
                HostName = targetSpace.Member.Name,
                HostPhoto = targetSpace.Member.Photo,
                CleaningOptionDict = cleaningOptionList,
                Introduction = targetSpace.Introduction,
                ShootingEquipment = targetSpace.ShootingEquipment,
                ParkingInfo = targetSpace.Parking,
                HostRule = targetSpace.HostRules,
                AmenityDict = amenityList,
                AmenityIconDict = a,
                Longitude = targetSpace.Longitude,
                Latitude = targetSpace.Latitude,
                TrafficInfo = targetSpace.Traffic,
                OperatingDayList = operationgDayList.ToList(),
                StartTimeList = startTimeList.ToList(),
                EndTimeList = endTimeList.ToList(),
                CancellationTitle = targetSpace.Cancellation.CancellationTitle,
                CancellationInfo = targetSpace.Cancellation.CancellationDetail,
                HoursForDiscount = hoursForDiscount,
                Discount = Decimal.Round((1 - discount), 2),
            };

            return result;
        }

        /// <summary>
        /// 收藏寫入資料庫
        /// </summary>
        /// <param name="bookingPageVM"></param>
        /// <param name="memberID"></param>
        public void CreateCollectionInDB(BookingPageViewModel bookingPageVM, string memberID)
        {
            var collection = new Collection
            {
                MemberID = int.Parse(memberID),
                SpaceID = bookingPageVM.SpaceBreifInfo.SpaceID,
            };

            _repository.Create<Collection>(collection);
            _repository.SaveChanges();
            _repository.Dispose();
        }

        /// <summary>
        /// 即時篩選
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<SearchingPageViewModel> GetFilteredSpaces(QueryViewModel query)
        {
            var city = query.City;
            var district = query.District;
            var Date = query.Date;
            var type = query.Type;
            var lowPrice = query.LowPrice;
            var highPrice = query.HighPrice;
            var attendees = query.Attendees;
            var area = query.Area;
            var amenities = query.Amenities;
            var keywords = query.Keywords;

            var scores = _repository.GetAll<Review>();
            var spaces = _repository.GetAll<Space>();
            var orders = _repository.GetAll<OrderDetail>();
            var spaceTypes = _repository.GetAll<SpaceType>();
            var operatings = _repository.GetAll<Operating>();
            var spaceAmenities = _repository.GetAll<SpaceAmenity>();


            if (!String.IsNullOrEmpty(city))
            {
                spaces = spaces.Where(x => x.City.CityName == city);
            }

            if (!String.IsNullOrEmpty(district))
            {
                spaces = spaces.Where(x => x.District.DistrictName == district);
            }

            if (!String.IsNullOrEmpty(type))
            {
                spaceTypes = spaceTypes.Where(x => x.TypeDetail.Type == type);
            }

            if (!String.IsNullOrEmpty(Date))
            {
                var startDate = DateTime.Parse(Date);
                var dayOfWeek = (int)startDate.DayOfWeek;
                if (dayOfWeek == 0)
                {
                    dayOfWeek = 7; 
                }
                operatings = operatings.Where(x => x.OperatingDay == dayOfWeek);
                orders = orders.Where(x => DateTime.Compare(x.StartDateTime, startDate) < 0 || DateTime.Compare(x.EndDateTime, startDate) > 0);
            }

            if (!String.IsNullOrEmpty(lowPrice))
            {
                var budget = decimal.Parse(lowPrice);
                spaces = spaces.Where(x => x.PricePerHour >= budget);
            }

            if (!String.IsNullOrEmpty(highPrice))
            {
                var budget = decimal.Parse(highPrice);
                spaces = spaces.Where(x => x.PricePerHour <= budget);
            }

            if (!String.IsNullOrEmpty(attendees))
            {
                var people = decimal.Parse(attendees);
                spaces = spaces.Where(x => x.Capacity >= people);
            }

            if (!String.IsNullOrEmpty(area))
            {
                var spaceArea = decimal.Parse(area);
                spaces = spaces.Where(x => x.MeasureOfArea >= spaceArea);
            }

            if (amenities != null && amenities.Count != 0)
            {
                spaceAmenities = spaceAmenities.Where(x => amenities.Contains(x.AmenityDetail.Amenity));
            }

            if (!String.IsNullOrEmpty(keywords))
            {
                var keywordArr = keywords.Split(' ');
                foreach (var keyword in keywordArr)
                {
                    spaces = spaces.Where(x => x.SpaceName.Contains(keyword));
                }
            }

            var filteredBySpace = spaces.Select(x => x);
            var filteredByType = spaceTypes.Select(x => x.Space).Distinct();
            var filteredByAmenity = spaceAmenities.Select(x => x.Space).Distinct();
            var filteredBytDate = operatings.Select(x => x.Space).Distinct();
            var unbookedSpaces = orders.Select(x => x.Order.Space).Distinct();
            var filterByDate = filteredBytDate.Union(unbookedSpaces);

            var insersectSpaces = filteredBySpace.Intersect(filteredByType).Intersect(filteredByAmenity).Intersect(filterByDate);

            var result = insersectSpaces.Select(x => new SearchingPageViewModel
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
                Scores = scores.Where(y => y.Order.SpaceID == x.SpaceID).Select(y => y.Score).ToList(),
            }).ToList();

            return result;
        }
    }
}