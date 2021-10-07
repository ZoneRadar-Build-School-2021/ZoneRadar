using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models;

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
        /// 產生搜尋頁篩選所需的JSON
        /// </summary>
        /// <returns></returns>
        public string GetFilterJSON()
        {
            var citiesAndDistricts = _repository.GetAll<District>().GroupBy(x => x.City).OrderBy(x => x.Key.CityID).ToDictionary(x => x.Key.CityName, x => x.Select(y => y.DistrictName).ToList());
            var spaceTypeList = _repository.GetAll<TypeDetail>().OrderBy(x => x.TypeDetailID).Select(x => x.Type).ToList();
            var amenityList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.Amenity).ToList();
            var result = new FilterViewModel
            {
                CityDistrictDictionary = citiesAndDistricts,
                SpaceTypeList = spaceTypeList,
                AmenityList = amenityList,
                SelectedCity = null,
                SelectedType = null,
            };

            var json = JsonConvert.SerializeObject(result);
            return json;
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
            // 建立轉換字典
            var amenityConvertDictionary = new Dictionary<string, string>
            {
                { "WiFi", "wifi" },
                { "桌子", "table" },
                { "椅子", "chair" },
                { "螢幕", "monitor" },
                { "投影幕", "screen" },
                { "數位電視", "apple_tv" },
                { "咖啡機", "coffee" },
                { "投影機", "projector" },
                { "白板", "whiteboard" },
                { "影印機", "printer" },
                { "大眾運輸工具", "public_transit" },
                { "戶外區", "outdoor_area" },
                { "廁所", "toilet" },
                { "無障礙空間", "handicap_access" },
                { "廚房", "kitchen" },
                { "包廂", "breakout_room" },
                { "停車場", "parking" },
            };
            var weekDayConvertDictoinary = new Dictionary<string, int>
            {
                { "星期一", 1 },
                { "星期二", 2 },
                { "星期三", 3 },
                { "星期四", 4 },
                { "星期五", 5 },
                { "星期六", 6 },
                { "星期日", 7 }
            };

            // 找出所有場地設施
            var originAmenityList = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.AmenityDetail);
            var convertedAmenityList = new List<AmenityDetail>();
            foreach (var item in originAmenityList)
            {
                convertedAmenityList.Add(new AmenityDetail
                {
                    Amenity = string.Concat(item.Amenity, amenityConvertDictionary.FirstOrDefault(x => x.Key == item.Amenity).Value),
                    AmenityCategoryDetail = item.AmenityCategoryDetail,
                    AmenityCategoryID = item.AmenityCategoryID,
                    AmenityDetailID = item.AmenityDetailID,
                    SpaceAmenity = item.SpaceAmenity,
                });
            }
            var amenityDictionary = convertedAmenityList.GroupBy(x => x.AmenityCategoryDetail.AmenityCategory).ToDictionary(x => x.Key, x => x.Select(y => y.Amenity).ToList());
            
            // 找出該場地所有營業資料
            var originOperatingList = _repository.GetAll<Operating>().Where(x => x.SpaceID == targetSpace.SpaceID).ToList();
            var convertedOperatingDayList = new List<string>();
            foreach (var day in originOperatingList.Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.OperatingDay).ToList())
            {
                convertedOperatingDayList.Add(weekDayConvertDictoinary.FirstOrDefault(x => x.Value == day).Key);
            }

            // 找出所有清潔公約選項
            var cleaningOptionList = _repository.GetAll<CleaningProtocol>().Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.CleaningOption).ToList();
            
            // 找出滿時優惠的時數
            var hoursForDiscount = _repository.GetAll<SpaceDiscount>().SingleOrDefault(x => x.SpaceID == targetSpace.SpaceID).Hour;
            
            // 找出滿時優惠的折數
            var discount = _repository.GetAll<SpaceDiscount>().SingleOrDefault(x => x.SpaceID == targetSpace.SpaceID).Discount;

            var result = new SpaceDetailViewModel
            {
                HostName = targetSpace.Member.Name,
                HostPhoto = targetSpace.Member.Photo,
                CleaningOptionDict = cleaningOptionList.GroupBy(x => x.CleaningCategory.Category).ToDictionary(x => x.Key, x => x.Select(y => y.OptionDetail).ToList()),
                Introduction = targetSpace.Introduction,
                ShootingEquipment = targetSpace.ShootingEquipment,
                ParkingInfo = targetSpace.Parking,
                HostRule = targetSpace.HostRules,
                AmenityDictionary = amenityDictionary,
                Longitude = targetSpace.Longitude,
                Latitude = targetSpace.Latitude,
                TrafficInfo = targetSpace.Traffic,
                OperatingDayList = convertedOperatingDayList,
                StartTimeList = originOperatingList.Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.StartTime).ToList(),
                EndTimeList = originOperatingList.Where(x => x.SpaceID == targetSpace.SpaceID).Select(x => x.EndTime).ToList(),
                CancellationTitle = targetSpace.Cancellation.CancellationTitle,
                CancellationInfo = targetSpace.Cancellation.CancellationDetail,
                HoursForDiscount = hoursForDiscount,
                Discount = discount,
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
        ///  便利設施 
        /// </summary>
        /// 分三類
        /// 1.

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
        ///  便利設施 
        /// </summary>
        /// 第二類
        /// 場地空間
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
        ///  便利設施 
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
        /// 取消政策 4種
        /// </summary>
        /// 
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

        /// <summary>
        /// 場地類型 16種 
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
        ///  清潔 4大類
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
        ///  第二類
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
        ///  第三類
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
        ///  第四類
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
        ///  營業時間
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
        ///  營業時間
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
        ///  編輯 讀資料庫裡的資料
        /// </summary>
        /// 參數是場地ID <param name="spaceId"></param>
        ///
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
                SomeOnesSpaceNameList=new List<SomeOnesSpaceName>(),
                SomeOnesSpaceIntroductionList=new List<SomeOnesSpaceIntroduction>(),
                SomeOnesMeasureOfAreaandCapacityList = new List<SomeOnesMeasureOfAreaandCapacity>(),
                SomeOnesPriceList=new List<SomeOnesPrice>(),
                SomeOnesDiscountsList=new List<SomeOnesDiscount>(),
                amenityAraeOneList=new List<AmenityAraeOne>(),
                amenityAraeTwoList=new List<AmenityAraeTwo>(),
                amenityAraeThreeList=new List<AmenityAraeThree>(),
                SomeOnesAmenityList=new List<SomeOnesAmenity>(),
                SomeOnesRulesList=new List<SomeOnesRules>(),
                SomeOnesTrafficList=new List<SomeOnesTraffic>(),
                SomeOnesParkingList=new List<SomeOnesParking>(),
                SomeOnesShootingList = new List<SomeOnesShooting>(),

            };
            ///地址 ///
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


            //活動類型
            //把活動類別有的撈出來
            List<SpaceType> spacetypes = _repository.GetAll<SpaceType>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spacetypes)
            {
                SomeOnesTypeDetail someOnesTypeDetail = new SomeOnesTypeDetail();
                someOnesTypeDetail.Type = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.Type).FirstOrDefault();
                someOnesTypeDetail.TypeDetailId = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.TypeDetailID).FirstOrDefault();
                result.SomeOnesTypeDetailList.Add(someOnesTypeDetail);
            }

            //把全部活動類別列出來
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
            //場地名稱
            var spaceName = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spaceName)
            {
                var someOnesSpaceTemp = new SomeOnesSpaceName()
                {
                    SpaceName=item.SpaceName
                };
                result.SomeOnesSpaceNameList.Add(someOnesSpaceTemp);
            }
            //場地簡介
            var spaceIntroduction = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spaceIntroduction)
            {
                var spaceIntroductiontemp = new SomeOnesSpaceIntroduction()
                {
                    Introduction=item.Introduction
                };

                result.SomeOnesSpaceIntroductionList.Add(spaceIntroductiontemp);
            };
            //場地大小人數
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

            /// 營業時間
            /// 
            /// 

            /// 定價
            /// 
            /// ///
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
            ///折扣 
            ///
            /// ///
            var someonsdiscounts = _repository.GetAll<SpaceDiscount>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in someonsdiscounts)
            {
                var someonsdiscountsTemp = new SomeOnesDiscount()
                {
                    SpaceId = item.SpaceID,
                    Discount = item.Discount,
                    Hours = item.Hour
                };
                result.SomeOnesDiscountsList.Add(someonsdiscountsTemp);
            }
            /// 撈有的設施
            /// 全部
            /// 
            var amenity = _repository.GetAll<SpaceAmenity>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            //var amenityone=amenity.Where(x=>x.)
            foreach (var item in amenity)
            {
                SomeOnesAmenity amenityTemp = new SomeOnesAmenity();
                amenityTemp.Amenity = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityDetailID == item.AmenityDetailID).Select(x => x.Amenity).FirstOrDefault();
                amenityTemp.AmenityId = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityDetailID == item.AmenityDetailID).Select(x => x.AmenityDetailID).FirstOrDefault();
                amenityTemp.AmenityCategoryID = _repository.GetAll<AmenityDetail>().Where(x => x.AmenityDetailID == item.AmenityDetailID).Select(x => x.AmenityCategoryID).FirstOrDefault();
                result.SomeOnesAmenityList.Add(amenityTemp);
            }

            /// 
            /// 場地條款
            /// 
            var rules = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in rules)
            {
                var rulesTemp = new SomeOnesRules()
                {
                    Rules=item.HostRules,
                };
                result.SomeOnesRulesList.Add(rulesTemp);
            }
            ///
            /// 交通資訊
            /// ///
            var traffic = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in traffic)
            {
                var trafficTemp = new SomeOnesTraffic()
                {
                    Traffic=item.Traffic,
                };
                result.SomeOnesTrafficList.Add(trafficTemp);
            }
            ///停車
            ///攝影
            ///
            var parking = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in parking)
            {
                var parkingTemp = new SomeOnesParking()
                {
                    Parking=item.Parking,
                };
                result.SomeOnesParkingList.Add(parkingTemp);
            }

            var shooting = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            foreach (var item in shooting)
            {
                var shootingTemp = new SomeOnesShooting()
                {
                    Shooting=item.ShootingEquipment
                };
                result.SomeOnesShootingList.Add(shootingTemp);
            }
            ///清潔條款細節
            /// 
            /// 
            //var cleanrule = _repository.GetAll<Space>().Where(x => x.SpaceID == spaceId).Select(x => x).ToList();
            //foreach (var item in cleanrule)
            //{
            //    var cleanRuleTemp = new SomeOnesCleanRule()
            //    {
            //        CleanRule=item.CleaningProtocol
            //    };
            //}
                
            return result;
        }

    }
}