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
        /// 根據ID找到指定場地(Steve)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Space GetSpaceByID(int? id)
        {
            var result = _repository.GetAll<Space>().SingleOrDefault(x => x.SpaceID == id);
            return result;
        }

        /// <summary>
        /// 找出精選場地(Jenny)
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
        /// 產生活動類型的SelectListItem(Jenny)
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
        /// 產生城市的SelectListItem(Jenny)
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
        /// 關閉資料庫連線(Jenny)
        /// </summary>
        public void DisposeCtx()
        {
            _repository.Dispose();
        }

        /// <summary>
        /// 找出場地簡短資訊(Steve)
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
        /// 找出場地詳細資訊(Steve)
        /// </summary>
        /// <param name="targetSpace"></param>
        /// <returns></returns>
        public SpaceDetailViewModel GetTargetSpaceDetail(Space targetSpace)
        {
            // 找出所有場地設施
            var amenityList = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.AmenityDetail);

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
            var originOperatingList = _repository.GetAll<Operating>().Where(x => x.SpaceID == targetSpace.SpaceID).ToList();
            var convertedOperatingDayList = new List<string>();
            foreach (var day in originOperatingList.Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.OperatingDay).ToList())
            {
                convertedOperatingDayList.Add(weekDayConverter.FirstOrDefault(x => x.Value == day).Key);
            }

            // 找出所有清潔公約選項
            var cleaningOptionList = _repository.GetAll<CleaningProtocol>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.CleaningOption)
                                     .GroupBy(x => x.CleaningCategory.Category);

            // 找出滿時優惠的時數
            var hoursForDiscount = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID) == null ? 0 : _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID).Hour;

            // 找出滿時優惠的折數
            var discount = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID) == null ? 0 : Decimal.Round(1 - (_repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID).Discount), 2);


            var result = new SpaceDetailViewModel
            {
                HostName = targetSpace.Member.Name,
                HostID = targetSpace.Member.MemberID,
                HostPhoto = targetSpace.Member.Photo,
                CleaningOptionDict = cleaningOptionList.ToDictionary(x => x.Key, x => x.Select(y => y.OptionDetail).ToList()),
                Introduction = targetSpace.Introduction,
                ShootingEquipment = targetSpace.ShootingEquipment,
                ParkingInfo = targetSpace.Parking,
                HostRule = targetSpace.HostRules,
                AmenityDict = amenityList.GroupBy(x => x.AmenityCategoryDetail.AmenityCategory).ToDictionary(x => x.Key, x => x.Select(y => y.Amenity).ToList()),
                AmenityIconDict = amenityList.ToDictionary(x => x.Amenity, x => x.AmenityICON),
                Longitude = targetSpace.Longitude,
                Latitude = targetSpace.Latitude,
                TrafficInfo = targetSpace.Traffic,
                OperatingDayList = convertedOperatingDayList,
                StartTimeList = originOperatingList.Select(x => x.StartTime.ToString(@"hh\:mm")).ToList(),
                EndTimeList = originOperatingList.Select(x => x.EndTime.ToString(@"hh\:mm")).ToList(),
                CancellationTitle = targetSpace.Cancellation.CancellationTitle,
                CancellationInfo = targetSpace.Cancellation.CancellationDetail,
                HoursForDiscount = hoursForDiscount,
                Discount = discount,
            };

            return result;
        }


        ///// <summary>
        ///// 收藏寫入資料庫(Steve)
        ///// </summary>
        ///// <param name="bookingPageVM"></param>
        ///// <param name="memberID"></param>
        //public void CreateCollectionInDB(BookingPageViewModel bookingPageVM, string memberID)
        //{
        //    var collection = new Collection
        //    {
        //        MemberID = int.Parse(memberID),
        //        SpaceID = bookingPageVM.SpaceBreifInfo.SpaceID,
        //    };

        //    _repository.Create<Collection>(collection);
        //    _repository.SaveChanges();
        //    _repository.Dispose();
        //}


        /// <summary>
        /// 搜尋頁即時篩選(Steve)
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
                spaces = spaces.Intersect(spaceTypes.Select(x => x.Space));
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
                orders = orders.Where(x => DateTime.Compare(x.StartDateTime, startDate) < 0 && DateTime.Compare(x.EndDateTime, startDate) > 0 && (x.Order.OrderStatusID == 2 || x.Order.OrderStatusID == 3));

                var filteredBytDate = operatings.Select(x => x.Space).Distinct();
                var unBookedSpaces = orders.Select(x => x.Order.Space).Distinct();
                var filterByDate = filteredBytDate.Except(unBookedSpaces);

                spaces = spaces.Intersect(filterByDate);
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
                foreach (var item in amenities)
                {
                    spaceAmenities = spaceAmenities.Where(x => x.AmenityDetail.Amenity == item);

                }

                var filteredByAmenity = spaceAmenities.Select(x => x.Space).Distinct();
                spaces = spaces.Intersect(filteredByAmenity);
            }

            if (!String.IsNullOrEmpty(keywords))
            {
                var keywordArr = keywords.Split(' ');
                foreach (var keyword in keywordArr)
                {
                    spaces = spaces.Where(x => x.SpaceName.ToUpper().Contains(keyword.ToUpper()));
                }
            }
            //var filteredBySpace = spaces.Select(x => x);
            //var filteredByType = spaceTypes.Select(x => x.Space).Distinct();
            //var filteredByAmenity = spaceAmenities.Select(x => x.Space).Distinct();
            //var filteredBytDate = operatings.Select(x => x.Space).Distinct();
            //var unBookedSpaces = orders.Select(x => x.Order.Space).Distinct();
            //var filterByDate = filteredBytDate.Union(unBookedSpaces);

            //var insersectSpaces = filteredBySpace.Intersect(filteredByType).Intersect(filteredByAmenity).Intersect(filterByDate).ToList();

            //var result = insersectSpaces.Select(x => new SearchingPageViewModel
            //{
            //    SpaceID = x.SpaceID,
            //    SpaceName = x.SpaceName,
            //    SpaceImageURLList = x.SpacePhoto.Where(y => y.SpaceID == x.SpaceID).Select(y => y.SpacePhotoUrl).ToList(),
            //    Address = x.Address,
            //    Capacity = x.Capacity,
            //    PricePerHour = x.PricePerHour,
            //    Country = x.City.CityName,
            //    City = x.City.CityName,
            //    District = x.District.DistrictName,
            //    MinHour = x.MinHours,
            //    MeasurementOfArea = x.MeasureOfArea,
            //    Scores = scores.Where(y => y.Order.SpaceID == x.SpaceID).Select(y => y.Score).ToList(),
            //}).ToList();
            //return result;
            var result = spaces.Select(x => new SearchingPageViewModel
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
        /// <summary>
        ///  找出memberName (Amber)
        /// </summary>
        
        //public SomeOnesSpaceViewModel ShowOwnerName()
        //{
        //    var result = new SomeOnesSpaceViewModel()
        //    {
        //        SomeOnesSpaceNameList=new List<SomeOnesSpaceName>(),
        //    };
        //    var owner = _repository.GetAll<Member>().Where(x => x.MemberID == 2).Select(x => x.Name).FirstOrDefault();
        //    foreach (var item in owner)
        //    {
        //        var ownerTemp = new SomeOnesSpaceName()
        //        {
        //            MamberName = item.ToString()
        //        };
        //        result.SomeOnesSpaceNameList.Add(ownerTemp);
        //    }
        //    return result;
        //}

        /// <summary>
        ///  找出便利設施 (Amber)
        /// </summary>
        /// 分三類
        /// 場地空間 第一類
        public SpaceViewModel ShowAmenityByIdOne()
        {
            var result = new SpaceViewModel()
            {
                amenityAraeOneList = new List<AmenityAraeOne>(),
            };
            var amenityOnes = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 1).Select(x => x).ToList();
            foreach (var amenityOne in amenityOnes)
            {
                var amenityOneTemp = new AmenityAraeOne()
                {
                    AmenityId = amenityOne.AmenityDetailID,
                    AmenityName = amenityOne.Amenity
                };
                result.amenityAraeOneList.Add(amenityOneTemp);
            };
            return result;
        }
        /// <summary>
        ///  找出便利設施 (Amber)
        /// </summary>
        /// 第二類
        public SpaceViewModel ShowAmenityByIdTwo()
        {
            var result = new SpaceViewModel()
            {
                amenityAraeTwoList = new List<AmenityAraeTwo>(),
            };
            var amenityTwos = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 2).Select(x => x).ToList();
            foreach (var amenityTwo in amenityTwos)
            {
                var amenityTwoTemp = new AmenityAraeTwo()
                {
                    AmenityId = amenityTwo.AmenityDetailID,
                    AmenityName = amenityTwo.Amenity
                };
                result.amenityAraeTwoList.Add(amenityTwoTemp);
            }
            return result;
        }
        /// <summary>
        ///  找出便利設施 (Amber)
        /// </summary>
        /// 第三類
        /// 其他
        public SpaceViewModel ShowAmenityByIdThree()
        {
            var result = new SpaceViewModel()
            {
                amenityAraeThreeList = new List<AmenityAraeThree>(),
            };
            var amenityThrees = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 3).Select(x => x).ToList();
            foreach (var amenityThree in amenityThrees)
            {
                var amenityThreeTemp = new AmenityAraeThree()
                {
                    AmenityId = amenityThree.AmenityDetailID,
                    AmenityName = amenityThree.Amenity
                };
                result.amenityAraeThreeList.Add(amenityThreeTemp);
            }
            return result;
        }


        /// <summary>
        /// 取消政策 4種(Amber)
        /// </summary>
        public SpaceViewModel ShowCancellations()
        {
            var result = new SpaceViewModel()
            {
                cancellationAraesList = new List<CancellationArae>(),
            };
            var cancels = _repository.GetAll<Cancellation>().Select(x => x).ToList();
            foreach (var cancel in cancels)
            {
                var canceltemp = new CancellationArae()
                {
                    CancellationTitle = cancel.CancellationTitle,
                    CancellationDetail = cancel.CancellationDetail
                };
                result.cancellationAraesList.Add(canceltemp);
            }
            return result;
        }
        /// </summary>
        /// 場地類型 16種 (Amber)
        /// </summary>
        public SpaceViewModel ShowSpaceType()
        {
            var result = new SpaceViewModel()
            {
                SpaceTypeAraeList = new List<SpaceTypeArae>()
            };
            var spaceTypes = _repository.GetAll<TypeDetail>().Select(x => x).ToList();
            foreach (var space in spaceTypes)
            {
                var typeTemp = new SpaceTypeArae()
                {
                    TypeDetailId = space.TypeDetailID,
                    Type = space.Type
                };
                result.SpaceTypeAraeList.Add(typeTemp);
            }
            return result;
        }

        /// <summary>
        ///  清潔 4大類 (Amber)
        ///  第一類  CleaningPolicy
        /// </summary>
        public SpaceViewModel ShowCleaningCategoryByIdOne()
        {
            var result = new SpaceViewModel()
            {
                CleanFisrtPartList = new List<CleanFisrtPart>()
            };
            var CleanFisrtParts = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 1).Select(x => x).ToList();
            foreach (var cleanOne in CleanFisrtParts)
            {
                var cleanOneTemp = new CleanFisrtPart()
                {
                    CleaningOptionId = cleanOne.CleaningOptionID,
                    OptionDetail = cleanOne.OptionDetail
                };
                result.CleanFisrtPartList.Add(cleanOneTemp);
            }
            return result;
        }
        /// <summary>
        ///  第二類 (Amber)
        /// </summary>
        public SpaceViewModel ShowCleaningCategoryByIdTwo()
        {
            var result = new SpaceViewModel()
            {
                CleanSecPartList = new List<CleanSecPart>()
            };
            var CleanSecParts = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 2).Select(x => x).ToList();
            foreach (var cleanSec in CleanSecParts)
            {
                var cleanSecTemp = new CleanSecPart()
                {
                    CleaningOptionId = cleanSec.CleaningOptionID,
                    OptionDetail = cleanSec.OptionDetail
                };
                result.CleanSecPartList.Add(cleanSecTemp);
            }
            return result;
        }
        /// <summary>
        /// 第三類 (Amber)
        /// </summary>
        public SpaceViewModel ShowCleaningCategoryByIdThree()
        {
            var result = new SpaceViewModel()
            {
                CleanThirdPartList = new List<CleanThirdPart>()
            };
            var CleanThirdParts = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 3).Select(x => x).ToList();
            foreach (var cleanThird in CleanThirdParts)
            {
                var cleanThirdTemp = new CleanThirdPart()
                {
                    CleaningOptionId = cleanThird.CleaningOptionID,
                    OptionDetail = cleanThird.OptionDetail
                };
                result.CleanThirdPartList.Add(cleanThirdTemp);
            }
            return result;
        }
        /// <summary>
        ///  第四類 (Amber)
        /// </summary>
        public SpaceViewModel ShowCleaningCategoryByIdFour()
        {
            var result = new SpaceViewModel()
            {
                CleanFourdPartList = new List<CleanFourdPart>()
            };
            var CleanFourdParts = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 4).Select(x => x).ToList();
            foreach (var cleanFourd in CleanFourdParts)
            {
                var cleanFourdTemp = new CleanFourdPart()
                {
                    CleaningOptionId = cleanFourd.CleaningOptionID,
                    OptionDetail = cleanFourd.OptionDetail
                };
                result.CleanFourdPartList.Add(cleanFourdTemp);
            }
            return result;
        }
        /// <summary>
        ///  自訂營業時間 (Amber)
        /// </summary>
        public List<SelectListItem> Operating()
        {
            var Operating = new List<SelectListItem>
            {
                new SelectListItem { Value = "06:00:00.0000000", Text = "06:00"},
                new SelectListItem { Value = "07:00:00.0000000", Text = "07:00"},
                new SelectListItem { Value = "08:00:00.0000000", Text = "08:00"},
                new SelectListItem { Value = "09:00:00.0000000", Text = "09:00"},
                new SelectListItem { Value = "10:00:00.0000000", Text = "10:00"},
                new SelectListItem { Value = "11:00:00.0000000", Text = "11:00"},
                new SelectListItem { Value = "12:00:00.0000000", Text = "12:00"},
                new SelectListItem { Value = "13:00:00.0000000", Text = "13:00"},
                new SelectListItem { Value = "14:00:00.0000000", Text = "14:00"},
                new SelectListItem { Value = "15:00:00.0000000", Text = "15:00"},
                new SelectListItem { Value = "16:00:00.0000000", Text = "16:00"},
                new SelectListItem { Value = "17:00:00.0000000", Text = "17:00"},
                new SelectListItem { Value = "18:00:00.0000000", Text = "18:00"},
                new SelectListItem { Value = "19:00:00.0000000", Text = "19:00"},
                new SelectListItem { Value = "20:00:00.0000000", Text = "20:00"},
                new SelectListItem { Value = "21:00:00.0000000", Text = "21:00"},
                new SelectListItem { Value = "22:00:00.0000000", Text = "22:00"},
                new SelectListItem { Value = "23:00:00.0000000", Text = "23:00"},
                new SelectListItem { Value = "00:00:00.0000000", Text = "00:00"},
            };
            return Operating;
        }
        /// <summary>
        ///  營業時間 天 (Amber)
        /// </summary>
        public List<SelectListItem> OperatingDay()
        {
            var Operatingday = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "星期一"},
                new SelectListItem { Value = "2", Text = "星期二"},
                new SelectListItem { Value = "3", Text = "星期三"},
                new SelectListItem { Value = "4", Text = "星期四"},
                new SelectListItem { Value = "5", Text = "星期五"},
                new SelectListItem { Value = "6", Text = "星期六"},
                new SelectListItem { Value = "7", Text = "星期日"},
            };
            return Operatingday;
        }

        /// <summary>
        /// 編輯 讀資料庫裡的資料 (Amber)
        /// </summary>
        public SomeOnesSpaceViewModel ReadAnySpace(int spaceId)
        {
            var result = new SomeOnesSpaceViewModel()
            {
                SomeOnesSpaceList = new List<SomeOnesSpace>(),
                SomeOnesCountryList = new List<SomeOnesCountry>(),
                SomeOnesDistrictList = new List<SomeOnesDistrict>(),
                SomeOnesCitytList = new List<SomeOnesCity>(),
                SomeOnesTypeDetailList = new List<SomeOnesTypeDetail>(),
                ShowAllTypeDetailList = new List<ShowAllTypeDetail>(),
                SomeOnesSpaceNameList = new List<SomeOnesSpaceName>(),
                SomeOnesSpaceIntroductionList = new List<SomeOnesSpaceIntroduction>(),
                SomeOnesMeasureOfAreaandCapacityList = new List<SomeOnesMeasureOfAreaandCapacity>(),
                SomeOnesPriceList = new List<SomeOnesPrice>(),
                SomeOnesDiscountsList = new List<SomeOnesDiscount>(),

                amenityAraeOneList = new List<AmenityAraeOne>(),
                amenityAraeTwoList = new List<AmenityAraeTwo>(),
                amenityAraeThreeList = new List<AmenityAraeThree>(),
                SomeOnesAmenityList = new List<SomeOnesAmenity>(),
                SomeTwoAmenityList = new List<SomeOnesAmenity>(),
                SomeThreeAmenityList = new List<SomeOnesAmenity>(),
                SomeOnesRulesList = new List<SomeOnesRules>(),
                SomeOnesTrafficList = new List<SomeOnesTraffic>(),
                SomeOnesParkingList = new List<SomeOnesParking>(),
                SomeOnesShootingList = new List<SomeOnesShooting>(),
                SomeOnesCancelAllList = new List<SomeOnesCancel>(),
                SomeOnesCancelList = new List<SomeOnesCancel>(),
                CleanRuleOptionsOneList = new List<SomeOnesCleanRule>(),
                CleanRuleOptionsTwoList = new List<SomeOnesCleanRule>(),
                CleanRuleOptionsThreeList = new List<SomeOnesCleanRule>(),
                CleanRuleOptionsFourList = new List<SomeOnesCleanRule>(),
                SomeOnesCleanRuleOneList = new List<SomeOnesCleanRule>(),
                SomeOnesCleanRuleTwoList = new List<SomeOnesCleanRule>(),
                SomeOnesCleanRuleThreeList = new List<SomeOnesCleanRule>(),
                SomeOnesCleanRuleFourList = new List<SomeOnesCleanRule>(),
                SpaceoperatingDaysList = new List<SpaceoperatingDay>(),
                _compareOperatingDay = new List<SpaceoperatingDay>(),
                Operating = new List<SelectListItem>(),
                SpaceOwnerNameList = new List<SomeOnesSpaceName>(),
            };
            /// <summary>
            ///找 地址( Amber) 
            /// </summary>

            var adds = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var add in adds)
            {
                var addsTemp = new SomeOnesSpace()
                {
                    Address = add.Address,
                    DistrictID = add.DistrictID,
                    Country = add.Country,
                };
                result.SomeOnesSpaceList.Add(addsTemp);
            }
            var country = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x.CountryID).FirstOrDefault();
            var countryName = _repository.GetAll<Country>().Where(x => x.CountryID == country).Select(x => x).ToList();
            foreach (var countryname in countryName)
            {
                var countryNameTemp = new SomeOnesCountry()
                {
                    CountryName = countryname.CountryName
                };
                result.SomeOnesCountryList.Add(countryNameTemp);
            };
            var district = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x.DistrictID).FirstOrDefault();
            var districtName = _repository.GetAll<District>().Where(x => x.DistrictID == district).Select(x => x).ToList();
            foreach (var districtname in districtName)
            {
                var districtNameTemp = new SomeOnesDistrict()
                {
                    DistrictName = districtname.DistrictName
                };
                result.SomeOnesDistrictList.Add(districtNameTemp);
            };

            var city = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x.CityID).FirstOrDefault();
            var cityName = _repository.GetAll<City>().Where(x => x.CityID == city).Select(x => x).ToList();
            foreach (var cityname in cityName)
            {
                var ciytTemp = new SomeOnesCity()
                {
                    CityName = cityname.CityName
                };
                result.SomeOnesCitytList.Add(ciytTemp);
            };
            ///<summary>
            //活動類型 把活動類別用戶有選的撈出來(Amber)
            /// </summary>

            List<SpaceType> spacetypes = _repository.GetAll<SpaceType>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spacetypes)
            {
                SomeOnesTypeDetail someOnesTypeDetail = new SomeOnesTypeDetail();
                someOnesTypeDetail.Type = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.Type).FirstOrDefault();
                someOnesTypeDetail.TypeDetailId = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.TypeDetailID).FirstOrDefault();
                result.SomeOnesTypeDetailList.Add(someOnesTypeDetail);
            }
            /// <summary>
            ///  把全部活動類別列出來(Amber )
            /// </summary>
            var showAllTypeDetail = _repository.GetAll<TypeDetail>().Select(x => x).ToList();

            foreach (var item in showAllTypeDetail)
            {
                var showAllTypeDetailTemp = new ShowAllTypeDetail()
                {
                    Type = item.Type,
                    TypeDetailId = item.TypeDetailID
                };
                result.ShowAllTypeDetailList.Add(showAllTypeDetailTemp);
            }
            /// <summary>
            /// 場地名稱( Amber )
            /// </summary>
            var spaceName = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x.SpaceName).ToList();
            foreach (var item in spaceName)
            {
                var someOnesSpaceTemp = new SomeOnesSpaceName()
                {
                    SpaceName = item
                };
                result.SomeOnesSpaceNameList.Add(someOnesSpaceTemp);
            }
            //場地主的名字
            var memberID = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).FirstOrDefault();
            var owner = _repository.GetAll<Member>().Where(x => x.MemberID == memberID.MemberID).Select(x => x.Name).ToList();
            foreach (var item in owner)
            {
                var someOnesSpaceTemp = new SomeOnesSpaceName()
                {
                    MamberName = item
                };
                result.SpaceOwnerNameList.Add(someOnesSpaceTemp);
            }
            /// <summary>
            ///  場地簡介(Amber )
            /// </summary>
            var spaceIntroduction = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spaceIntroduction)
            {
                var spaceIntroductiontemp = new SomeOnesSpaceIntroduction()
                {
                    Introduction = item.Introduction
                };

                result.SomeOnesSpaceIntroductionList.Add(spaceIntroductiontemp);
            };
            /// <summary>
            ///  場地大小人數(Amber) 
            /// </summary>
            var spaceMeasureOfAreaandCapacity = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spaceMeasureOfAreaandCapacity)
            {
                var MeasureOfAreaandCapacityTemp = new SomeOnesMeasureOfAreaandCapacity()
                {
                    Capacity = item.Capacity,
                    MeasureOfArea = item.MeasureOfArea
                };
                result.SomeOnesMeasureOfAreaandCapacityList.Add(MeasureOfAreaandCapacityTemp);
            }
            /// <summary>
            ///  定價(Amber)
            /// </summary>
            var someonesprice = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in someonesprice)
            {
                var someonespriceTemp = new SomeOnesPrice()
                {
                    MinHours = item.MinHours,
                    PricePerHour = item.PricePerHour

                };
                result.SomeOnesPriceList.Add(someonespriceTemp);
            }
            /// <summary>
            ///  折扣(Amber)
            /// </summary>
            var someonsdiscounts = _repository.GetAll<SpaceDiscount>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in someonsdiscounts)
            {
                var someonsdiscountsTemp = new SomeOnesDiscount()
                {
                    SpaceId = item.SpaceID,
                    Discount = 100 * (item.Discount),
                    Hours = item.Hour
                };
                result.SomeOnesDiscountsList.Add(someonsdiscountsTemp);
            }
            /// <summary>
            /// 撈用戶有選的設施( Amber)
            /// </summary>

            var amenitys = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == spaceId).Select(x => x.AmenityDetailID).ToList();
            var AmenityDetails = _repository.GetAll<AmenityDetail>().ToList();
            var AmenityOptions = new List<AmenityDetail>();
            foreach (var item in amenitys)
            {
                var amenityDetails = AmenityDetails.First(x => x.AmenityDetailID == item);
                AmenityOptions.Add(amenityDetails);
            }
            /// <summary>
            ///  便利設施(Amber)
            /// </summary>
            var Amenity = AmenityOptions.Where(x => x.AmenityCategoryID == 1).Select(x => x.Amenity).ToList();
            foreach (var item in Amenity)
            {
                var temp = new SomeOnesAmenity()
                {
                    Amenity = item
                };
                result.SomeOnesAmenityList.Add(temp);
            }
            var AmenityTwo = AmenityOptions.Where(x => x.AmenityCategoryID == 2).Select(x => x.Amenity).ToList();
            foreach (var item in AmenityTwo)
            {
                var temp = new SomeOnesAmenity()
                {
                    Amenity = item
                };
                result.SomeTwoAmenityList.Add(temp);
            }
            var AmenityThree = AmenityOptions.Where(x => x.AmenityCategoryID == 3).Select(x => x.Amenity).ToList();
            foreach (var item in AmenityThree)
            {
                var temp = new SomeOnesAmenity()
                {
                    Amenity = item
                };
                result.SomeThreeAmenityList.Add(temp);
            }
            /// <summary>
            /// 便利全部設施選項( Amber )
            /// </summary>
            var AmenityOptionOnes = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 1).ToList();
            foreach (var item in AmenityOptionOnes)
            {
                var temp = new AmenityAraeOne()
                {
                    AmenityName = item.Amenity,
                };
                result.amenityAraeOneList.Add(temp);
            }
            /// <summary>
            ///便利全部場地空間選項( Amber ) 
            /// </summary>
            var AmenityOptionTwo = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 2).ToList();
            foreach (var item in AmenityOptionTwo)
            {
                var temp = new AmenityAraeTwo()
                {
                    AmenityName = item.Amenity,
                };
                result.amenityAraeTwoList.Add(temp);
            }
            /// <summary>
            /// 其他場地空間選項 (Amber)
            /// </summary>
            var AmenityOptionThree = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 3).ToList();
            foreach (var item in AmenityOptionThree)
            {
                var temp = new AmenityAraeThree()
                {
                    AmenityName = item.Amenity,
                };
                result.amenityAraeThreeList.Add(temp);
            }
            /// <summary>
            /// 場地條款( Amber )
            /// </summary>
            var rules = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in rules)
            {
                var rulesTemp = new SomeOnesRules()
                {
                    Rules = item.HostRules,
                };
                result.SomeOnesRulesList.Add(rulesTemp);
            }
            /// <summary>
            ///  交通資訊(Amber) 
            /// </summary>
            var traffic = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in traffic)
            {
                var trafficTemp = new SomeOnesTraffic()
                {
                    Traffic = item.Traffic,
                };
                result.SomeOnesTrafficList.Add(trafficTemp);
            }
            /// <summary>
            ///  停車(Amber) 
            /// </summary>
            var parking = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in parking)
            {
                var parkingTemp = new SomeOnesParking()
                {
                    Parking = item.Parking,
                };
                result.SomeOnesParkingList.Add(parkingTemp);
            }
            /// <summary>
            ///  攝影(Amber )
            /// </summary>
            var shooting = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in shooting)
            {
                if (item.ShootingEquipment == null)
                {
                    var shootingTemp = new SomeOnesShooting()
                    {
                        Shooting = item.ShootingEquipment,
                        Displaynone="d-none"
                    };
                    result.SomeOnesShootingList.Add(shootingTemp);
                }
                else
                {
                    var shootingTemp = new SomeOnesShooting()
                    {
                        Shooting = item.ShootingEquipment,
                        Displaynone = " "
                    };
                    result.SomeOnesShootingList.Add(shootingTemp);
                }
            }
            /// <summary>
            ///  取消政策 全部(Amber )
            /// </summary>
            var cancels = _repository.GetAll<Cancellation>().Select(x => x).ToList();
            foreach (var item in cancels)
            {
                var cancelsTemp = new SomeOnesCancel()
                {
                    CancellationID = item.CancellationID,
                    CancellationTitle = item.CancellationTitle,
                    CancellationDetail = item.CancellationDetail
                };
                result.SomeOnesCancelAllList.Add(cancelsTemp);
            }
            /// <summary>
            ///  取消政策 被選的(Amber )
            /// </summary>
            var cancelsSelectId = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in cancelsSelectId)
            {
                SomeOnesCancel cancelTemp = new SomeOnesCancel();
                cancelTemp.CancellationID = _repository.GetAll<Cancellation>().Where(x => x.CancellationID == item.CancellationID).Select(x => x.CancellationID).FirstOrDefault();
                cancelTemp.CancellationTitle = _repository.GetAll<Cancellation>().Where(x => x.CancellationID == item.CancellationID).Select(x => x.CancellationTitle).FirstOrDefault();
                cancelTemp.CancellationDetail = _repository.GetAll<Cancellation>().Where(x => x.CancellationID == item.CancellationID).Select(x => x.CancellationDetail).FirstOrDefault();
                result.SomeOnesCancelList.Add(cancelTemp);
            }
            /// <summary>
            ///  清潔條款細節(Amber) 
            /// </summary>
            var cleanallspace = _repository.GetAll<CleaningProtocol>().Where(x => x.SpaceID == spaceId).Select(x => x.CleaningOptionID).ToList();
            var cleaningOptions = _repository.GetAll<CleaningOption>().ToList();
            var spaceOptions = new List<CleaningOption>();
            foreach (var item in cleanallspace)
            {
                var cleanOption = cleaningOptions.First(x => x.CleaningOptionID == item);
                spaceOptions.Add(cleanOption);
            }
            /// <summary>
            ///  同一場地 第一類 (Amber) 
            /// </summary>
            var fistcleans = spaceOptions.Where(x => x.CleaningCategoryID == 1).Select(x => x.OptionDetail).ToList();
            foreach (var item in fistcleans)
            {
                var temp = new SomeOnesCleanRule()
                {
                    OptionDetail = item
                };
                result.SomeOnesCleanRuleOneList.Add(temp);
            }
            /// <summary>
            ///  第二類(Amber) 
            /// </summary>
            var Seccleans = spaceOptions.Where(x => x.CleaningCategoryID == 2).Select(x => x.OptionDetail).ToList();
            foreach (var item in Seccleans)
            {
                var temp = new SomeOnesCleanRule()
                {
                    OptionDetail = item
                };
                result.SomeOnesCleanRuleTwoList.Add(temp);
            }
            /// <summary>
            ///   第三類(Amber) 
            /// </summary>
            var Thirdcleans = spaceOptions.Where(x => x.CleaningCategoryID == 3).Select(x => x.OptionDetail).ToList();
            foreach (var item in Thirdcleans)
            {
                var temp = new SomeOnesCleanRule()
                {
                    OptionDetail = item
                };
                result.SomeOnesCleanRuleThreeList.Add(temp);
            }
            /// <summary>
            ///  第四類(Amber) 
            /// </summary>
            var Fourcleans = spaceOptions.Where(x => x.CleaningCategoryID == 4).Select(x => x.OptionDetail).ToList();
            foreach (var item in Fourcleans)
            {
                var temp = new SomeOnesCleanRule()
                {
                    OptionDetail = item
                };
                result.SomeOnesCleanRuleFourList.Add(temp);
            }
            /// <summary>
            /// 選項一類( Amber) 
            /// </summary>
            var cleansAllone = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 1).ToList();
            foreach (var item in cleansAllone)
            {
                var cleanRuleTemp = new SomeOnesCleanRule()
                {
                    CleaningCategoryID = item.CleaningCategoryID,
                    CleaningOptionID = item.CleaningOptionID,
                    OptionDetail = item.OptionDetail
                };
                result.CleanRuleOptionsOneList.Add(cleanRuleTemp);
            }
            /// <summary>
            /// 選項二類(Amber) 
            /// </summary>
            var cleansAlltwo = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 2).ToList();
            foreach (var item in cleansAlltwo)
            {
                var cleanRuleTemp = new SomeOnesCleanRule()
                {
                    CleaningCategoryID = item.CleaningCategoryID,
                    CleaningOptionID = item.CleaningOptionID,
                    OptionDetail = item.OptionDetail
                };
                result.CleanRuleOptionsTwoList.Add(cleanRuleTemp);
            }
            /// <summary>
            /// 選項三類(Amber) 
            /// </summary>
            var cleansAllthree = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 3).ToList();
            foreach (var item in cleansAllthree)
            {
                var cleanRuleTemp = new SomeOnesCleanRule()
                {
                    CleaningCategoryID = item.CleaningCategoryID,
                    CleaningOptionID = item.CleaningOptionID,
                    OptionDetail = item.OptionDetail
                };
                result.CleanRuleOptionsThreeList.Add(cleanRuleTemp);
            }
            /// <summary>
            /// 選項四類 (Amber) 
            /// </summary>
            var cleansAllfour = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 4).ToList();
            foreach (var item in cleansAllfour)
            {
                var cleanRuleTemp = new SomeOnesCleanRule()
                {
                    CleaningCategoryID = item.CleaningCategoryID,
                    CleaningOptionID = item.CleaningOptionID,
                    OptionDetail = item.OptionDetail
                };
                result.CleanRuleOptionsFourList.Add(cleanRuleTemp);
            }
            /// <summary>
            /// 營業時間 有被選的 (Amber) 
            /// </summary>
            var openDays = _repository.GetAll<Operating>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in openDays)
            {
                var operatingday = new SpaceoperatingDay()
                {
                    SpaceId = item.SpaceID,
                    OperatingDay = item.OperatingDay,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                };
                if (operatingday.OperatingDay == 1)
                {
                    operatingday.weekDay = "星期一";
                }
                else if (operatingday.OperatingDay == 2)
                {
                    operatingday.weekDay = "星期二";
                }
                else if (operatingday.OperatingDay == 3)
                {
                    operatingday.weekDay = "星期三";
                }
                else if (operatingday.OperatingDay == 4)
                {
                    operatingday.weekDay = "星期四";
                }
                else if (operatingday.OperatingDay == 5)
                {
                    operatingday.weekDay = "星期五";
                }
                else if (operatingday.OperatingDay == 6)
                {
                    operatingday.weekDay = "星期六";
                }
                else if (operatingday.OperatingDay == 7)
                {
                    operatingday.weekDay = "星期日";
                }

                result.SpaceoperatingDaysList.Add(operatingday);
            }
            List<SpaceoperatingDay> _compareOperatingDay = new List<SpaceoperatingDay>();
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 1, weekDay = "星期一" });
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 2, weekDay = "星期二" });
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 3, weekDay = "星期三" });
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 4, weekDay = "星期四" });
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 5, weekDay = "星期五" });
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 6, weekDay = "星期六" });
            result._compareOperatingDay.Add(new SpaceoperatingDay { OperatingDay = 7, weekDay = "星期日" });

            /// <summary>
            /// 增加場地 增加地址的datamodel轉viewmodel (Amber) 
            /// </summary>
            
        


            return result;

        }
        //public AddSpaceViewModel CreateSpace()
        //{
        //    var result
        //};

        /// <summary>
        /// 找出特定場地的Booking Card資料(Steve)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BookingCardViewModel GetTargetBookingCard(int? id)
        {
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
            var originOperatingList = _repository.GetAll<Operating>().Where(x => x.SpaceID == id).ToList();
            var convertedOperatingDayList = new List<string>();
            foreach (var day in originOperatingList.Where(x => x.SpaceID == id).Select(x => x.OperatingDay).ToList())
            {
                convertedOperatingDayList.Add(weekDayConverter.FirstOrDefault(x => x.Value == day).Key);
            }

            //var startTimeList = _repository.GetAll<Operating>().Where(x => x.SpaceID == id).Select(x => x.StartTime.ToString(@"hh\:mm")).ToList();
            //var endTimeList = _repository.GetAll<Operating>().Where(x => x.SpaceID == id).Select(x => x.EndTime.ToString(@"hh\:mm")).ToList();
            var hoursForDiscount = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == id).Hour;
            var discount = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == id).Discount;
            var minHour = _repository.GetAll<Space>().FirstOrDefault(x => x.SpaceID == id).MinHours;

            var result = new BookingCardViewModel
            {
                OperatingDayList = convertedOperatingDayList,
                StartTimeList = originOperatingList.Select(x => x.StartTime.ToString(@"hh\:mm")).ToList(),
                EndTimeList = originOperatingList.Select(x => x.EndTime.ToString(@"hh\:mm")).ToList(),
                HoursForDiscount = hoursForDiscount,
                Discount = Decimal.Round((1 - discount), 2),
                MinHour = minHour,
            };

            return result;
        }
    }
}