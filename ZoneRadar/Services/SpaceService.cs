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

        internal object ShowSpaceSelect()
        {
            throw new NotImplementedException();
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
                    AmenityId=amenityOne.AmenityDetailID,
                    AmenityName=amenityOne.Amenity
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
                cancellationAraesList=new List<CancellationArae>(),
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
                SpaceTypeAraeList=new List<SpaceTypeArae>()
            };
            var spaceTypes = _repository.GetAll<TypeDetail>().Select(x => x).ToList();
            foreach (var space in spaceTypes) 
            {
                var typeTemp = new SpaceTypeArae()
                {
                    TypeDetailId = space.TypeDetailID,
                    Type=space.Type
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
                CleanFisrtPartList =new List<CleanFisrtPart>()
            };
            var CleanFisrtParts = _repository.GetAll<CleaningOption>().Where(x => x.CleaningCategoryID == 1).Select(x => x).ToList();
            foreach (var cleanOne in CleanFisrtParts) 
            {
                var cleanOneTemp = new CleanFisrtPart()
                {
                    CleaningOptionId=cleanOne.CleaningOptionID,
                    OptionDetail=cleanOne.OptionDetail
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
        public SomeOnesSpaceViewModel ReadAnySpace(int spaceId)
        {
            var result = new SomeOnesSpaceViewModel()
            {
                SomeOnesSpaceList = new List<SomeOnesSpace>(),
                SomeOnesCountryList = new List<SomeOnesCountry>(),
                SomeOnesDistrictList=new List<SomeOnesDistrict>(),
                SomeOnesCitytList=new List<SomeOnesCity>(),
                SomeOnesTypeDetailList=new List<SomeOnesTypeDetail>()
            };
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
            foreach (var cityname in cityName ) 
            {
                var ciytTemp = new SomeOnesCity()
                {
                    CityName=cityname.CityName
                };
                result.SomeOnesCitytList.Add(ciytTemp);
            };
            List<SpaceType> spacetypes =  _repository.GetAll<SpaceType>().Where(x => x.SpaceID == spaceId).ToList();
            foreach (var item in spacetypes)
            {
               
                SomeOnesTypeDetail someOnesTypeDetail=  new SomeOnesTypeDetail();
                someOnesTypeDetail.Type = _repository.GetAll<TypeDetail>().Where(x => x.TypeDetailID == item.TypeDetailID).Select(x => x.Type).FirstOrDefault(); ;
                result.SomeOnesTypeDetailList.Add(someOnesTypeDetail);
            }
       
            return result;
        }
    }

}