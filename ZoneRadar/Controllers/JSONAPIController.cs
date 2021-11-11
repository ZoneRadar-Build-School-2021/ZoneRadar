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
    [RoutePrefix("webapi/spaces")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JSONAPIController : ApiController
    {
        private readonly SpaceService _spaceService;
        private readonly ReviewService _reviewService;
        private readonly PreOrderService _preOrderService;
        private readonly ZONERadarRepository _repository;
        private FilterViewModel _filterDataFromIndex;
        public JSONAPIController()
        {
            _spaceService = new SpaceService();
            _reviewService = new ReviewService();
            _preOrderService = new PreOrderService();
            _repository = new ZONERadarRepository();
            _filterDataFromIndex = new FilterViewModel();
        }

        /// <summary>
        /// 從首頁取得filterData(Steve)
        /// </summary>
        /// <param name="filterVm"></param>
        /// <returns></returns>
        [Route("GetFilterDataFromIndex")]
        [AcceptVerbs("POST")]
        public APIResponse GetFilterDataFromIndex(FilterViewModel filterVm)
        {
            var response = new APIResponse();
            try
            {
                _filterDataFromIndex.SelectedCity = filterVm.SelectedCity;
                _filterDataFromIndex.SelectedType = filterVm.SelectedType;
                _filterDataFromIndex.SelectedDate = filterVm.SelectedDate;

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = _filterDataFromIndex;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 取得Filter資訊資料API(Steve)
        /// </summary>
        /// <returns></returns>
        [Route("GetFilterData")]
        [AcceptVerbs("GET")]
        public APIResponse GetFilterData(string type, string city, string date)
        {
            var response = new APIResponse();
            try
            {
                var citiesAndDistricts = _repository.GetAll<District>().GroupBy(x => x.City).OrderBy(x => x.Key.CityID);
                var spaceTypeList = _repository.GetAll<TypeDetail>().OrderBy(x => x.TypeDetailID).Select(x => x.Type);
                var amenityList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.Amenity);
                var amenityIconList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.AmenityICON);

                var result = new FilterViewModel
                {
                    CityDistrictDictionary = citiesAndDistricts.ToDictionary(x => x.Key.CityName, x => x.Select(y => y.DistrictName).ToList()),
                    SpaceTypeList = spaceTypeList.ToList(),
                    AmenityList = amenityList.ToList(),
                    AmenityIconList = amenityIconList.ToList(),
                    SelectedCity = city == null ? "" : city,
                    SelectedType = type == null ? "" : type,
                    SelectedDate = date == null ? "" : date,
                };

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = result;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 取得搜尋頁場地資訊API(Steve)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("GetFilteredSpaces")]
        [AcceptVerbs("GET", "POST")]
        public APIResponse GetFilteredSpaces(QueryViewModel query)
        {
            var response = new APIResponse();
            try
            {
                var queriedSpaces = _spaceService.GetFilteredSpaces(query);

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = queriedSpaces;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 取得預購卡所需資料(Steve)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetBookingCardData")]
        [AcceptVerbs("GET")]
        public APIResponse GetBookingCardData(int? id)
        {
            var response = new APIResponse();
            try
            {
                var result = _spaceService.GetTargetBookingCard(id);
                if (User.Identity.IsAuthenticated)
                {
                    var memberID = int.Parse(User.Identity.Name);
                    var isCollection = _repository.GetAll<Collection>().FirstOrDefault(x => x.SpaceID == id && x.MemberID == memberID) == null ? false : true;
                    result.IsCollection = isCollection;
                }
                else
                {
                    result.IsCollection = false;
                }

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = result;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 確認會員是否登入
        /// </summary>
        /// <returns></returns>
        [Route("CheckLogin")]
        [AcceptVerbs("GET")]
        public APIResponse CheckLogin()
        {
            var response = new APIResponse();
            try
            {
                bool isLogin = User.Identity.IsAuthenticated;

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = isLogin;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 加入購物車
        /// </summary>
        /// <param name="preOrderVM"></param>
        /// <returns></returns>
        [Route("AddPreOrder")]
        [AcceptVerbs("POST")]
        public APIResponse AddPreOrder(PreOrderViewModel preOrderVM)
        {
            var response = new APIResponse();
            try
            {
                int memberID = int.Parse(User.Identity.Name);
                _preOrderService.PlaceAPreOrder(preOrderVM, memberID);

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 加入收藏
        /// </summary>
        /// <param name="spaceID"></param>
        /// <returns></returns>
        [Route("AddCollection")]
        [AcceptVerbs("POST")]
        public APIResponse AddCollection(SpaceBriefViewModel SpaceBriefVM)
        {
            var response = new APIResponse();
            try
            {
                int memberID = int.Parse(User.Identity.Name);
                _spaceService.AddToCollection(SpaceBriefVM.SpaceID, memberID);

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 移除收藏
        /// </summary>
        /// <param name="spaceID"></param>
        /// <returns></returns>
        [Route("RemoveCollection")]
        [AcceptVerbs("POST")]
        public APIResponse RemoveCollection(SpaceBriefViewModel SpaceBriefVM)
        {
            var response = new APIResponse();
            try
            {
                int memberID = int.Parse(User.Identity.Name);
                _spaceService.RemoveFromCollection(SpaceBriefVM.SpaceID, memberID);

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 及時計算價錢
        /// </summary>
        /// <param name="preOrderVM"></param>
        /// <returns></returns>
        [Route("Calculate")]
        [AcceptVerbs("POST")]
        public APIResponse Calculate(PreOrderViewModel preOrderVM)
        {
            var response = new APIResponse();
            try
            {
                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = _preOrderService.CalculatePrice(preOrderVM);

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 取得cloudinary參數(Steve)
        /// </summary>
        /// <returns></returns>
        [Route("GetUploadPrams")]
        [AcceptVerbs("GET")]
        public APIResponse GetUploadPrams(int? id)
        {
            var response = new APIResponse();
            try
            {
                response.Status = "Success";
                response.Message = string.Empty;
                // id須由外部傳入
                response.Response = _spaceService.GetSpacePhotoFromDB(id);

                return response;

            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 將上傳照片存入資料庫(Steve)
        /// </summary>
        /// <param name="SaveSpacePhotosVM"></param>
        /// <returns></returns>
        [Route("SavePhotos")]
        [AcceptVerbs("POST")]
        public APIResponse SavePhotos(SaveSpacePhotosViewModel SaveSpacePhotosVM)
        {
            var response = new APIResponse();
            try
            {
                _spaceService.ReflashSpacePhotoFromDB(SaveSpacePhotosVM);

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }

        /// <summary>
        /// 取得場地更多評價(Steve)
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        [Route("GetMoreReview")]
        [AcceptVerbs("GET")]
        public APIResponse GetMoreReview(int id, int sentCount, int addCount)
        {
            var response = new APIResponse();
            try
            {
                var reviewList = _reviewService.GetTargetSpaceMoreReviews(id, sentCount, addCount);

                response.Status = "Success";
                response.Message = string.Empty;
                response.Response = reviewList;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = "Fail";
                response.Message = ex.Message;
                response.Response = null;

                return response;
            }
        }
    }
}
