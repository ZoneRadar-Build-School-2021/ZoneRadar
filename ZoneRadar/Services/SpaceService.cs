using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Enums;
using System.Data.Entity;

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
            var result = _repository.GetAll<Space>().SingleOrDefault(x => x.SpaceID == id && x.SpaceStatusID == 2);
            return result;
        }

        /// <summary>
        /// 找出精選場地(Jenny)
        /// </summary>
        /// <returns></returns>
        public List<SelectedSpaceViewModel> GetSelectedSpace()
        {
            //找出針對場地的評論
            var reviews = _repository.GetAll<Review>().Where(x => x.ToHost).ToList();
            //找出有評論的訂單(訂單會是已完成狀態)
            var orders = reviews.Select(x => x.Order);
            //找出有訂單、有評論且上架的場地
            var spaces = orders.Select(x => x.Space).Where(x => x.SpaceStatusID == 2).Distinct();
            //找出場地的城市名稱
            var cities = spaces.Select(x => x.City).Distinct();
            //找出場地的圖片
            var photos = spaces.Select(x => x.SpacePhoto.First(y => y.Sort == 1));

            //var spacePhotos = _repository.GetAll<SpacePhoto>().ToList();

            var selectedSpaces = new List<SelectedSpaceViewModel>();

            foreach (var item in spaces)
            {
                //計算場地平均分數
                //找出該場地所有訂單的orderID
                var orderIDs = orders.Where(x => x.SpaceID == item.SpaceID).Select(x => x.OrderID);
                //找出該場地的所有評論並計算平均分數
                var scoreAvg = reviews.Where(x => orderIDs.Contains(x.OrderID)).Average(x => x.Score);

                //場地圖片資料表還沒建好，先寫防呆程式
                //var spacePhoto = spacePhotos.FirstOrDefault(x => x.SpaceID == item.SpaceID);
                //var spacePhotoUrl = spacePhoto == null ? "" : spacePhoto.SpacePhotoUrl;
                selectedSpaces.Add(
                    new SelectedSpaceViewModel
                    {
                        SpaceId = item.SpaceID,
                        CityName = cities.First(x => x.CityID == item.CityID ).CityName,
                        Capacity = item.Capacity,
                        PricePerHour = item.PricePerHour,
                        SpacePhoto = photos.First(x=>x.SpaceID == item.SpaceID).SpacePhotoUrl,
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
                Value = x.Type,
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
                Value = x.CityName,
                Text = x.CityName
            }).ToList();

            return cityOptions;
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

            // 找出滿時優惠的時數與折數
            var spaceDiscounts = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == targetSpace.SpaceID);

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
                HoursForDiscount = spaceDiscounts == null ? 0 : spaceDiscounts.Hour,
                Discount = spaceDiscounts == null ? 0 : decimal.Round(1 - spaceDiscounts.Discount, 2),
                HostEmail = targetSpace.Member.Email,
                OtherAmenity = targetSpace.OtherAmenity
            };

            return result;
        }

        /// <summary>
        /// 收藏寫入資料庫(Steve)
        /// </summary>
        /// <param name="bookingPageVM"></param>
        /// <param name="memberID"></param>
        public void AddToCollection(int spaceID, int memberID)
        {
            var collection = new Collection
            {
                MemberID = memberID,
                SpaceID = spaceID,
            };

            _repository.Create<Collection>(collection);
            _repository.SaveChanges();
            _repository.Dispose();
        }

        /// <summary>
        /// 從資料庫中移除收藏(Steve)
        /// </summary>
        /// <param name="bookingPageVM"></param>
        /// <param name="memberID"></param>
        public void RemoveFromCollection(int spaceID, int memberID)
        {
            var target = _repository.GetAll<Collection>().FirstOrDefault(x => x.SpaceID == spaceID && x.MemberID == memberID);

            _repository.Delete<Collection>(target);
            _repository.SaveChanges();
            _repository.Dispose();
        }

        /// <summary>
        /// 搜尋頁即時篩選(Steve)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<SearchingPageViewModel> GetFilteredSpaces(QueryViewModel query)
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

            var scores = _repository.GetAll<Review>().Where(x => x.ToHost == true);
            var spaces = _repository.GetAll<Space>().Where(x => x.SpaceStatusID == 2);
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
                var theDate = DateTime.Parse(Date);
                var pickedStartDate = theDate.Date;
                var pickedEndDate = pickedStartDate.AddDays(1).AddSeconds(-1);
                var dayOfWeek = (int)theDate.DayOfWeek;
                if (dayOfWeek == 0)
                {
                    dayOfWeek = 7;
                }

                // 當天有營業
                var filteredBytDate = operatings.Where(x => x.OperatingDay == dayOfWeek).Select(x => x.Space).Distinct();
                // 篩選時間有成立訂單(篩選時間在訂單的開始與結束時間之間)
                var bookedSpaces = orders.Where(x => pickedStartDate < x.StartDateTime && pickedEndDate > x.EndDateTime).Select(x => x.Order.Space).Distinct();
                // 取得當天有營業且沒有訂單的場地
                var filterByDate = filteredBytDate.Except(bookedSpaces);

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
                var filteredByAmenity = spaceAmenities.Where(x => amenities.Contains(x.AmenityDetail.Amenity));

                var spaceIDs = filteredByAmenity.Select(x => x.SpaceID).Distinct();
                foreach (var amenity in amenities)
                {
                    var layer = filteredByAmenity.Where(x => x.AmenityDetail.Amenity == amenity).Select(x => x.SpaceID);
                    spaceIDs = spaceIDs.Intersect(layer);
                }

                spaces = spaces.Where(x => spaceIDs.Contains(x.SpaceID));
            }

            if (!String.IsNullOrEmpty(keywords))
            {
                var keywordArr = keywords.Split(' ');
                foreach (var keyword in keywordArr)
                {
                    spaces = spaces.Where(x => x.SpaceName.ToUpper().Contains(keyword.ToUpper()));
                }
            }

            var result = spaces.OrderByDescending(x => x.Order.Count).Select(x => new SearchingPageViewModel
            {
                SpaceID = x.SpaceID,
                SpaceName = x.SpaceName,
                SpaceImageURLList = x.SpacePhoto.Where(y => y.SpaceID == x.SpaceID).OrderBy(y => y.Sort).Select(y => y.SpacePhotoUrl).ToList(),
                Address = x.Address,
                Capacity = x.Capacity,
                PricePerHour = x.PricePerHour,
                Country = x.City.CityName,
                City = x.City.CityName,
                District = x.District.DistrictName,
                MinHour = x.MinHours,
                MeasurementOfArea = x.MeasureOfArea,
                Scores = scores.Where(y => y.Order.SpaceID == x.SpaceID).Select(y => y.Score).ToList(),
            });
            return result;
        }

        /// <summary>
        ///  找出memberName (Amber)
        /// </summary>
        public SpaceViewModel GetSpaceId() 
        {
            var space = (_repository.GetAll<Space>().Max(x => x.SpaceID))+1;
            var spacID = new SpaceViewModel()
            {
                SpaceId = space
            };
            return spacID;
        }

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
            var amenitys = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityCategoryID == 1).Select(x => x).ToList();
            foreach (var amenityOne in amenitys)
            {
                var amenityTemp = new AmenityAraeOne()
                {
                    AmenityId = amenityOne.AmenityDetailID,
                    AmenityName = amenityOne.Amenity
                };
                result.amenityAraeOneList.Add(amenityTemp);
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
                    CancellationDetail = cancel.CancellationDetail,
                    CancellationId = cancel.CancellationID
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
            var Operating = new List<SelectListItem> {
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
                SpaceOwnerNameList = new List<SomeOnesSpaceName>(),
                Operating = new List<SelectListItem>(),
            };
          
        //找 地址( Amber) 
        var adds = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var add in adds)
            {
                var addsTemp = new SomeOnesSpace()
                {
                    Address = add.Address,
                    DistrictID = add.DistrictID,
                    Country = add.Country,
                    SpaceID = add.SpaceID,
                    OtherAmenitys=add.OtherAmenity,
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
            //讀經緯
            var lat = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x.Latitude).FirstOrDefault();
            var lng = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x.Longitude).FirstOrDefault();

            foreach (var cityname in cityName)
            {
                var ciytTemp = new SomeOnesCity()
                {
                    CityName = cityname.CityName,
                    Lat = lat,
                    Lng = lng
                };
                result.SomeOnesCitytList.Add(ciytTemp);
            };

        
            //活動類型 把活動類別用戶有選的撈出來(Amber)


            List <SpaceType> spacetypes = _repository.GetAll<SpaceType>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spacetypes)
            {
                SomeOnesTypeDetail someOnesTypeDetail = new SomeOnesTypeDetail();
                someOnesTypeDetail.Type = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.Type).FirstOrDefault();
                someOnesTypeDetail.TypeDetailId = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.TypeDetailID).FirstOrDefault();
                result.SomeOnesTypeDetailList.Add(someOnesTypeDetail);
            }
            //  把全部活動類別列出來(Amber )
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

            //  場地簡介(Amber )
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
                    Discount = Math.Floor(10m * (1m - (item.Discount))),
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
            var Amenity = AmenityOptions.Where(x => x.AmenityCategoryID == 1).ToList();
            foreach (var item in Amenity)
            {
                var temp = new SomeOnesAmenity()
                {
                    Amenity = item.Amenity,
                    AmenityId = item.AmenityDetailID,

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
                    AmenityId = item.AmenityDetailID
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
                    AmenityId = item.AmenityDetailID
                };
                result.amenityAraeTwoList.Add(temp);
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
                        Displaynone = "d-none"
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
            ///// <summary>
            ///// 營業時間 有被選的 (Amber) 
            int[] OperatingDays = { 1, 2, 3, 4, 5, 6, 7 };

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
                //1.判斷資料庫中該spaceID一到日是否有營業
                if (OperatingDays.Contains(operatingday.OperatingDay))
                {
                    //有營業 營業的checkbox checked
                    operatingday.isOpen = "checked";
                    //2.判斷是全天還是小時
                    //全天的開時間跟結束時間
                    TimeSpan start = new TimeSpan(6, 0, 0);
                    TimeSpan end = new TimeSpan(23, 0, 0);
                    if (operatingday.StartTime.Equals(start) && operatingday.EndTime.Equals(end))
                    {
                        //符合全天的條件全天的 radiobox checked 
                        operatingday.isAllDayCheck = "checked";
                    }
                    else
                    {
                        //反之
                        operatingday.isHourCheck = "checked";
                    }
                    //每個要的值
                    if (operatingday.OperatingDay == 1)
                    {
                        operatingday.WeekDay = "星期一";
                        operatingday.AllDayValue = "Y1";
                        operatingday.HoursValue = "hr1";
                        operatingday.TagName = "Hours1";
                    }
                    else if (operatingday.OperatingDay == 2)
                    {
                        operatingday.WeekDay = "星期二";
                        operatingday.AllDayValue = "Y2";
                        operatingday.HoursValue = "hr2";
                        operatingday.TagName = "Hours2";
                    }
                    else if (operatingday.OperatingDay == 3)
                    {
                        operatingday.WeekDay = "星期三";
                        operatingday.AllDayValue = "Y3";
                        operatingday.HoursValue = "hr3";
                        operatingday.TagName = "Hours3";
                    }
                    else if (operatingday.OperatingDay == 4)
                    {
                        operatingday.WeekDay = "星期四";
                        operatingday.AllDayValue = "Y4";
                        operatingday.HoursValue = "hr4";
                        operatingday.TagName = "Hours4";
                    }
                    else if (operatingday.OperatingDay == 5)
                    {
                        operatingday.WeekDay = "星期五";
                        operatingday.AllDayValue = "Y5";
                        operatingday.HoursValue = "hr5";
                        operatingday.TagName = "Hours5";
                    }
                    else if (operatingday.OperatingDay == 6)
                    {
                        operatingday.WeekDay = "星期六";
                        operatingday.AllDayValue = "Y6";
                        operatingday.HoursValue = "hr6";
                        operatingday.TagName = "Hours6";
                    }
                    else
                    {
                        operatingday.WeekDay = "星期日";
                        operatingday.AllDayValue = "Y7";
                        operatingday.HoursValue = "hr7";
                        operatingday.TagName = "Hours7";
                    }
                }
                else
                {
                    operatingday.isOpen = "";
                    if (operatingday.OperatingDay == 1)
                    {
                        operatingday.WeekDay = "星期一";
                        operatingday.AllDayValue = "Y1";
                        operatingday.HoursValue = "hr1";
                        operatingday.TagName = "Hours1";
                    }
                    else if (operatingday.OperatingDay == 2)
                    {
                        operatingday.WeekDay = "星期二";
                        operatingday.AllDayValue = "Y2";
                        operatingday.HoursValue = "hr2";
                        operatingday.TagName = "Hours2";
                    }
                    else if (operatingday.OperatingDay == 3)
                    {
                        operatingday.WeekDay = "星期三";
                        operatingday.AllDayValue = "Y3";
                        operatingday.HoursValue = "hr3";
                        operatingday.TagName = "Hours3";
                    }
                    else if (operatingday.OperatingDay == 4)
                    {
                        operatingday.WeekDay = "星期四";
                        operatingday.AllDayValue = "Y4";
                        operatingday.HoursValue = "hr4";
                        operatingday.TagName = "Hours4";
                    }
                    else if (operatingday.OperatingDay == 5)
                    {
                        operatingday.WeekDay = "星期五";
                        operatingday.AllDayValue = "Y5";
                        operatingday.HoursValue = "hr5";
                        operatingday.TagName = "Hours5";
                    }
                    else if (operatingday.OperatingDay == 6)
                    {
                        operatingday.WeekDay = "星期六";
                        operatingday.AllDayValue = "Y6";
                        operatingday.HoursValue = "hr6";
                        operatingday.TagName = "Hours6";
                    }
                    else
                    {
                        operatingday.WeekDay = "星期日";
                        operatingday.AllDayValue = "Y7";
                        operatingday.HoursValue = "hr7";
                        operatingday.TagName = "Hours7";
                    }

                }
                result.SpaceoperatingDaysList.Add(operatingday);

            }

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
            };
            return result;
        }
        /// <summary>
        /// 增加場地 增加地址的datamodel (Amber) 
        /// </summary>

        public AddSpaceViewModel CreateSpace(AddSpaceViewModel addSpaceViewModel)

        {
            var spacecity = addSpaceViewModel.CityID;
            var city = _repository.GetAll<City>().Where(x => x.CityName == spacecity).Select(x => x.CityID).FirstOrDefault();
            var space = new Space
            {
                MemberID = addSpaceViewModel.MemberID,
                SpaceName = addSpaceViewModel.SpaceName,
                Introduction = addSpaceViewModel.Introduction,
                MeasureOfArea = addSpaceViewModel.MeasureOfArea,
                Capacity = addSpaceViewModel.Capacity,
                PricePerHour = addSpaceViewModel.PricePerHour,
                MinHours = addSpaceViewModel.MinHours,
                HostRules = addSpaceViewModel.HostRules,
                Traffic = addSpaceViewModel.Traffic,
                Parking = addSpaceViewModel.Parking,
                ShootingEquipment = addSpaceViewModel.ShootingEquipment,
                CancellationID = addSpaceViewModel.CancellationID,
                CountryID = addSpaceViewModel.CountryID,
                CityID = city,
                DistrictID = addSpaceViewModel.DistrictID,
                Address = addSpaceViewModel.Address,
                PublishTime = DateTime.Today,
                Latitude = addSpaceViewModel.Lat,
                Longitude = addSpaceViewModel.Lng,
                SpaceStatusID = 2,
                OtherAmenity=addSpaceViewModel.OtherAmenity,

            };

            _repository.Create<Space>(space);
            _repository.SaveChanges();

            var spaceid = _repository.GetAll<Space>().Where(x => x.SpaceName== addSpaceViewModel.SpaceName).Select(x => x.SpaceID).FirstOrDefault();
            var spaceDiscount=new SpaceDiscount
            {
                SpaceID = spaceid,
                Hour = addSpaceViewModel.Hour,
                Discount=1m-((addSpaceViewModel.Discount)/10.00m),
            };
            List<SpacePhoto> imgs = new List<SpacePhoto>();
            var y = 1;
            foreach (var item in addSpaceViewModel.SpacePhotoUrl)
            {
                imgs.Add(new SpacePhoto
                {
                    SpaceID = spaceid,
                    SpacePhotoUrl = item,
                    Sort = y
                });
                y++;
            }


            List<SpaceType> type = new List<SpaceType>();
            foreach (var item in addSpaceViewModel.TypeDetailID)
            {
                type.Add(new SpaceType { SpaceID = spaceid, TypeDetailID = item });
            }
            List<CleaningProtocol> cleaningProtocol = new List<CleaningProtocol>();
            foreach (var item in addSpaceViewModel.CleaningOptionID)
            {
                cleaningProtocol.Add(new CleaningProtocol { SpaceID = spaceid, CleaningOptionID = item });
            }
            List<SpaceAmenity> spaceAmenity = new List<SpaceAmenity>();
            foreach (var item in addSpaceViewModel.AmenityDetailID)
            {
                spaceAmenity.Add(new SpaceAmenity { SpaceID = spaceid, AmenityDetailID = item });
            }

            //營業時間
            List<string> hours = new List<string>();
            hours.Add(addSpaceViewModel.Hours1);
            hours.Add(addSpaceViewModel.Hours2);
            hours.Add(addSpaceViewModel.Hours3);
            hours.Add(addSpaceViewModel.Hours4);
            hours.Add(addSpaceViewModel.Hours5);
            hours.Add(addSpaceViewModel.Hours6);
            hours.Add(addSpaceViewModel.Hours7);
            hours = hours.OfType<string>().ToList();


            List<Operating> ope = new List<Operating>();
            foreach (var item in addSpaceViewModel.OperatingDay)
            {
                var weekday = new Operating { OperatingDay = item, SpaceID = spaceid };
                ope.Add(weekday);
            }
            for (int i = 0; i < ope.Count; i++)
            {
                if (hours[i].Contains("Y"))
                {
                    ope[i].StartTime = TimeSpan.Parse("06:00");
                    ope[i].EndTime = TimeSpan.Parse("23:00");
                }
                else
                {
                    int x = 0;
                    ope[i].StartTime = TimeSpan.Parse(addSpaceViewModel.StartTime[x]);
                    ope[i].EndTime = TimeSpan.Parse(addSpaceViewModel.EndTime[x]);
                    x++;
                }
            }
            _repository.CreateRange<SpacePhoto>(imgs);
            _repository.CreateRange<Operating>(ope);
            _repository.CreateRange<SpaceType>(type);
            _repository.CreateRange<CleaningProtocol>(cleaningProtocol);
            _repository.CreateRange<SpaceAmenity>(spaceAmenity);
            _repository.Create<SpaceDiscount>(spaceDiscount);
            _repository.SaveChanges();
            return addSpaceViewModel;
        }


        /// <summary>
        ///  場地修改(Amber)
        /// </summary>
        public AddSpaceViewModel EditSpace(AddSpaceViewModel addSpaceViewModel)
        {
            var spacecity = addSpaceViewModel.CityID;
            var city = _repository.GetAll<City>().Where(x => x.CityName == spacecity).Select(x => x.CityID).FirstOrDefault();

            var spaceUpdate = _repository.GetAll<Space>().Where(x => x.SpaceID == addSpaceViewModel.SpaceID).FirstOrDefault();
            spaceUpdate.SpaceID = addSpaceViewModel.SpaceID;
            spaceUpdate.MemberID = addSpaceViewModel.MemberID;
            spaceUpdate.SpaceName = addSpaceViewModel.SpaceName;
            spaceUpdate.Introduction = addSpaceViewModel.Introduction;
            spaceUpdate.MeasureOfArea = addSpaceViewModel.MeasureOfArea;
            spaceUpdate.Capacity = addSpaceViewModel.Capacity;
            spaceUpdate.PricePerHour = addSpaceViewModel.PricePerHour;
            spaceUpdate.MinHours = addSpaceViewModel.MinHours;
            spaceUpdate.HostRules = addSpaceViewModel.HostRules;
            spaceUpdate.Traffic = addSpaceViewModel.Traffic;
            spaceUpdate.Parking = addSpaceViewModel.Parking;
            spaceUpdate.ShootingEquipment = addSpaceViewModel.ShootingEquipment;
            spaceUpdate.CancellationID = addSpaceViewModel.CancellationID;
            spaceUpdate.CountryID = addSpaceViewModel.CountryID;
            spaceUpdate.CityID = city;
            spaceUpdate.DistrictID = addSpaceViewModel.DistrictID;
            spaceUpdate.Address = addSpaceViewModel.Address;
            spaceUpdate.PublishTime = _repository.GetAll<Space>().Where(x => x.SpaceID == addSpaceViewModel.SpaceID).Select(x => x.PublishTime).FirstOrDefault();
            spaceUpdate.Latitude = addSpaceViewModel.Lat;
            spaceUpdate.Longitude = addSpaceViewModel.Lng;
            spaceUpdate.OtherAmenity = addSpaceViewModel.OtherAmenity;

            _repository.Update<Space>(spaceUpdate);
            _repository.SaveChanges();

            //var spaceid = _repository.GetAll<Space>().Max(x => x.SpaceID);
            var SpaceDiscountID = _repository.GetAll<SpaceDiscount>().First(x => x.SpaceID == addSpaceViewModel.SpaceID);

            SpaceDiscountID.Hour = addSpaceViewModel.Hour;
            SpaceDiscountID.Discount = 1m - ((addSpaceViewModel.Discount) / 10.00m);
                
            
            _repository.Update<SpaceDiscount>(SpaceDiscountID);
            _repository.SaveChanges();

           


            var typeOld = _repository.GetAll<SpaceType>().Where(x => x.SpaceID == addSpaceViewModel.SpaceID).ToList();

            foreach (var item in typeOld)
            {
                _repository.Delete<SpaceType>(item);
                _repository.SaveChanges();
            }
           

            List<SpaceType> type = new List<SpaceType>();
            foreach (var item in addSpaceViewModel.TypeDetailID)
            {
                type.Add(new SpaceType { SpaceID = addSpaceViewModel.SpaceID, TypeDetailID = item });
            }

            _repository.CreateRange<SpaceType>(type);
            _repository.SaveChanges();

           
            var cleaningProtocolOld = _repository.GetAll<CleaningProtocol>().Where(x => x.SpaceID == addSpaceViewModel.SpaceID).ToList();
            foreach (var item in cleaningProtocolOld)
            {
                _repository.Delete<CleaningProtocol>(item);
                _repository.SaveChanges();
            }
            List<CleaningProtocol> cleaningProtocol = new List<CleaningProtocol>();
            foreach (var item in addSpaceViewModel.CleaningOptionID)
            {
                cleaningProtocol.Add(new CleaningProtocol { SpaceID = addSpaceViewModel.SpaceID, CleaningOptionID = item });
            }
            _repository.CreateRange<CleaningProtocol>(cleaningProtocol);
            _repository.SaveChanges();

            var spaceAmenityOld = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == addSpaceViewModel.SpaceID).ToList();
            foreach (var item in spaceAmenityOld)
            {
                _repository.Delete<SpaceAmenity>(item);
                _repository.SaveChanges();
            }
            List<SpaceAmenity> spaceAmenity = new List<SpaceAmenity>();
            foreach (var item in addSpaceViewModel.AmenityDetailID)
            {
                spaceAmenity.Add(new SpaceAmenity { SpaceID = addSpaceViewModel.SpaceID, AmenityDetailID = item });
            }
            _repository.CreateRange<SpaceAmenity>(spaceAmenity);
            _repository.SaveChanges();

            var OperatingOld = _repository.GetAll<Operating>().Where(x => x.SpaceID == addSpaceViewModel.SpaceID).ToList();
            foreach (var item in OperatingOld)
            {
                _repository.Delete<Operating>(item);
                _repository.SaveChanges();

            }

            //營業時間
            List<string> hours = new List<string>();
            hours.Add(addSpaceViewModel.Hours1);
            hours.Add(addSpaceViewModel.Hours2);
            hours.Add(addSpaceViewModel.Hours3);
            hours.Add(addSpaceViewModel.Hours4);
            hours.Add(addSpaceViewModel.Hours5);
            hours.Add(addSpaceViewModel.Hours6);
            hours.Add(addSpaceViewModel.Hours7);
            hours = hours.OfType<string>().ToList();


            List<Operating> ope = new List<Operating>();
            foreach (var item in addSpaceViewModel.OperatingDay)
            {
                var weekday = new Operating { OperatingDay = item, SpaceID = addSpaceViewModel.SpaceID };
                ope.Add(weekday);
            }
            for (int i = 0; i < ope.Count; i++)
            {
                if (hours[i].Contains("Y"))
                {
                    ope[i].StartTime = TimeSpan.Parse("06:00");
                    ope[i].EndTime = TimeSpan.Parse("23:00");
                }
                else
                {
                    //int x = 0;
                    ope[i].StartTime = TimeSpan.Parse(addSpaceViewModel.StartTime[i]);
                    ope[i].EndTime = TimeSpan.Parse(addSpaceViewModel.EndTime[i]);
                    //x++;
                }
            }
            _repository.CreateRange<Operating>(ope);
            _repository.SaveChanges();

           
            
            return addSpaceViewModel;
        }

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
            var operatingList = _repository.GetAll<Operating>().Where(x => x.SpaceID == id).ToList();
            var spaceDiscounts = _repository.GetAll<SpaceDiscount>().FirstOrDefault(x => x.SpaceID == id);
            var targetSpace = _repository.GetAll<Space>().FirstOrDefault(x => x.SpaceID == id);
            var orderTimeList = _repository.GetAll<OrderDetail>().Where(x => x.Order.SpaceID == id && x.Order.OrderStatusID != 5).ToList().Select(x => x.StartDateTime.ToString("yyyy-MM-dd"));

            var result = new BookingCardViewModel
            {
                OperatingDayList = operatingList.Select(x => x.OperatingDay).ToList(),
                StartTimeList = operatingList.Select(x => x.StartTime.ToString(@"hh\:mm")).ToList(),
                EndTimeList = operatingList.Select(x => x.EndTime.ToString(@"hh\:mm")).ToList(),
                HoursForDiscount = spaceDiscounts == null ? 0 : spaceDiscounts.Hour,
                Discount = spaceDiscounts == null ? 1 : decimal.Round((1 - spaceDiscounts.Discount), 2),
                MinHour = targetSpace.MinHours,
                PricePerHour = (int)targetSpace.PricePerHour,
                Capacity = targetSpace.Capacity,
                OrderTimeList = orderTimeList.ToList(),
                Latitude = targetSpace.Latitude,
                Longitude = targetSpace.Longitude,
            };

            return result;
        }


        /// <summary>
        /// 取得場地主擁有的場地(Jenny)
        /// </summary>
        public List<SpaceManageViewModel> GetHostSpace(int userId)
        {
            var spaces = _repository.GetAll<Space>().Where(x => x.MemberID == userId).ToList();
            var spaceManageList = new List<SpaceManageViewModel>();

            foreach (var space in spaces)
            {
                //計算場地平均分數
                var spaceReview = space.Order.Select(x => x.Review.FirstOrDefault(y => y.ToHost)).OfType<Review>().ToList();
                double scoreAvg = spaceReview.Count == 0 ? 0 : spaceReview.Average(x => x.Score);

                //計算場地近30天的被預訂次數
                var orderDetails = new List<OrderDetail>();
                var allorderEndDates = new List<DateTime>();
                foreach (var order in space.Order)
                {
                    //計算場地近30天的被預訂次數
                    var details = order.OrderDetail.Where(x => x.StartDateTime.Date.AddDays(30) >= DateTime.UtcNow.AddHours(8).Date);
                    orderDetails.AddRange(details);

                    //找出最後被預定日期並加一天
                    var dates = order.OrderDetail.Select(x => x.EndDateTime.AddDays(1));
                    allorderEndDates.AddRange(dates);
                }
                var lastOrderdDate = allorderEndDates.Count == 0 ? "today" : allorderEndDates.Max().ToString("yyyy/MM/dd");

                spaceManageList.Add(new SpaceManageViewModel
                {
                    SpaceID = space.SpaceID,
                    SpacePhotoUrl = space.SpacePhoto.FirstOrDefault(x => x.Sort == 1) == null ? "" : space.SpacePhoto.First(x => x.Sort == 1)
                    .SpacePhotoUrl,
                    SpaceName = space.SpaceName,
                    SpaceAddress = string.Concat(space.District.DistrictID.ToString(), space.City.CityName, space.District.DistrictName, space.Address),
                    Score = scoreAvg,
                    NumberOfReviews = spaceReview.Count,
                    NumberOfOrders = orderDetails.Count,
                    SpaceStatusId = space.SpaceStatusID,
                    CanDiscontinueDate = lastOrderdDate,
                    DiscontinuedDate = space.DiscontinuedDate.HasValue ? space.DiscontinuedDate.Value.ToString("yyyy/MM/dd") : "無"
                });
            }

            return spaceManageList;
        }

        /// <summary>
        /// 將場地狀態資訊存進資料庫(下架、刪除、重新上架)(Jenny)
        /// </summary>
        public SweetAlert SetSpaceStatus(SpaceStatusInformation spaceStatusInfo)
        {
            var sweetAlert = new SweetAlert { Alert = false };
            try
            {
                var space = _repository.GetAll<Space>().FirstOrDefault(x => x.SpaceID == spaceStatusInfo.SpaceId && x.MemberID == spaceStatusInfo.UserId);
                if (space != null)
                {
                    //儲存預定下架日期
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.Discontinued && spaceStatusInfo.DiscontinuedDate != null)
                    {
                        space.DiscontinuedDate = spaceStatusInfo.DiscontinuedDate.Value;
                        //如果預定下架日期是當天，直接將場地狀態改成下架
                        if (spaceStatusInfo.DiscontinuedDate.Value.Date == DateTime.Today)
                        {
                            space.SpaceStatusID = (int)SpaceStatusEnum.Discontinued;
                        }
                    }
                    //取消下架
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.Discontinued && spaceStatusInfo.DiscontinuedDate == null)
                    {
                        space.DiscontinuedDate = null;
                    }
                    //刪除場地
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.Delete)
                    {
                        space.SpaceStatusID = (int)SpaceStatusEnum.Delete;
                    }
                    //重新上架
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.OnTheShelf)
                    {
                        space.SpaceStatusID = (int)SpaceStatusEnum.OnTheShelf;
                        space.DiscontinuedDate = null;
                    }

                    _repository.Update(space);
                    _repository.SaveChanges();
                    return sweetAlert;
                }
                else
                {
                    sweetAlert.Alert = true;
                    sweetAlert.Message = "您並非該場地主";
                    sweetAlert.Icon = false;
                    return sweetAlert;
                }
            }
            catch (Exception ex)
            {
                sweetAlert.Alert = true;
                sweetAlert.Message = "發生錯誤，請稍後再試！";
                sweetAlert.Icon = false;
                return sweetAlert;
            }
        }
  

        /// <summary>
        /// 取得該編輯場地的照片
        /// </summary>
        /// <returns></returns>
        public SpacePhotoViewModel GetSpacePhotoFromDB(int? id)
        {
            var urlList = _repository.GetAll<SpacePhoto>().Where(x => x.SpaceID == id).Select(x => x.SpacePhotoUrl);

            var result = new SpacePhotoViewModel()
            {
                Name = "dt6vz3pav",
                Preset = "c3caow1j",
                PhotoUrlList = urlList.ToList(),
            };

            return result;
        }


        /// <summary>
        /// 儲存照片至資料庫(需確認是否可以每次都刪除舊的資料)
        /// </summary>
        /// <param name="SaveSpacePhotosVM"></param>
        public void ReflashSpacePhotoFromDB(SaveSpacePhotosViewModel SaveSpacePhotosVM)
        {
            var spaceID = SaveSpacePhotosVM.SpaceID;
            var urlList = SaveSpacePhotosVM.PhotoUrlList;
           
            var originPhotos = _repository.GetAll<SpacePhoto>().Where(x => x.SpaceID == spaceID).ToList();
            {
                foreach (var entity in originPhotos)
                {
                    _repository.Delete<SpacePhoto>(entity);
                }
            }

            var photoList = urlList.Select(x => new SpacePhoto
            {
                SpaceID = spaceID,
                Sort = urlList.IndexOf(x) + 1,
                SpacePhotoUrl = x

            });


            _repository.CreateRange<SpacePhoto>(photoList);
            _repository.SaveChanges();
            _repository.Dispose();
        }
    }
}