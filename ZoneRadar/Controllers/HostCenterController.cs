using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Controllers
{
    public class HostCenterController : Controller
    {
        // GET: HostCenter
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  場地主上架場地(Amber) 
        /// </summary>
        public ActionResult AddSpace()
        {
            var model = new SpaceViewModel
            {
                SpaceTypeAraeList = _spaceService.ShowSpaceType().SpaceTypeAraeList,
                cancellationAraesList = _spaceService.ShowCancellations().cancellationAraesList,
                addressAraeList = _spaceService.ShowAmenityByIdOne().addressAraeList,

                amenityAraeOneList = _spaceService.ShowAmenityByIdOne().amenityAraeOneList,
                amenityAraeTwoList = _spaceService.ShowAmenityByIdTwo().amenityAraeTwoList,
                amenityAraeThreeList = _spaceService.ShowAmenityByIdThree().amenityAraeThreeList,

                CleanFisrtPartList = _spaceService.ShowCleaningCategoryByIdOne().CleanFisrtPartList,
                CleanSecPartList = _spaceService.ShowCleaningCategoryByIdTwo().CleanSecPartList,
                CleanThirdPartList = _spaceService.ShowCleaningCategoryByIdThree().CleanThirdPartList,
                CleanFourdPartList = _spaceService.ShowCleaningCategoryByIdFour().CleanFourdPartList,

                Operating = _spaceService.Operating(),

            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpace(SpaceViewModel spaceVM)
        {
            var model = new SpaceViewModel
            {

            };
            return View(model);
        }

        /// <summary>
        /// 場地主編輯場地 (Amber)
        /// </summary>
        public ActionResult EditSpace(int spaceId)
        {
            var model = new SomeOnesSpaceViewModel()
            {
                SomeOnesSpaceList = _spaceService.ReadAnySpace(spaceId).SomeOnesSpaceList,
                SomeOnesCountryList = _spaceService.ReadAnySpace(spaceId).SomeOnesCountryList,
                SomeOnesDistrictList = _spaceService.ReadAnySpace(spaceId).SomeOnesDistrictList,
                SomeOnesCitytList = _spaceService.ReadAnySpace(spaceId).SomeOnesCitytList,
                SomeOnesTypeDetailList = _spaceService.ReadAnySpace(spaceId).SomeOnesTypeDetailList,
                ShowAllTypeDetailList = _spaceService.ReadAnySpace(spaceId).ShowAllTypeDetailList,
                SomeOnesSpaceNameList = _spaceService.ReadAnySpace(spaceId).SomeOnesSpaceNameList,
                SomeOnesSpaceIntroductionList = _spaceService.ReadAnySpace(spaceId).SomeOnesSpaceIntroductionList,
                SomeOnesMeasureOfAreaandCapacityList = _spaceService.ReadAnySpace(spaceId).SomeOnesMeasureOfAreaandCapacityList,
                SomeOnesPriceList = _spaceService.ReadAnySpace(spaceId).SomeOnesPriceList,
                SomeOnesDiscountsList = _spaceService.ReadAnySpace(spaceId).SomeOnesDiscountsList,
                SomeOnesRulesList = _spaceService.ReadAnySpace(spaceId).SomeOnesRulesList,
                SomeOnesTrafficList = _spaceService.ReadAnySpace(spaceId).SomeOnesTrafficList,
                SomeOnesParkingList = _spaceService.ReadAnySpace(spaceId).SomeOnesParkingList,
                SomeOnesShootingList = _spaceService.ReadAnySpace(spaceId).SomeOnesShootingList,
                SomeOnesCancelAllList = _spaceService.ReadAnySpace(spaceId).SomeOnesCancelAllList,
                SomeOnesCancelList = _spaceService.ReadAnySpace(spaceId).SomeOnesCancelList,

                SomeOnesAmenityList = _spaceService.ReadAnySpace(spaceId).SomeOnesAmenityList,
                amenityAraeOneList = _spaceService.ReadAnySpace(spaceId).amenityAraeOneList,

                SomeTwoAmenityList = _spaceService.ReadAnySpace(spaceId).SomeTwoAmenityList,
                amenityAraeTwoList = _spaceService.ReadAnySpace(spaceId).amenityAraeTwoList,

                SomeThreeAmenityList = _spaceService.ReadAnySpace(spaceId).SomeThreeAmenityList,
                amenityAraeThreeList = _spaceService.ReadAnySpace(spaceId).amenityAraeThreeList,

                CleanRuleOptionsOneList = _spaceService.ReadAnySpace(spaceId).CleanRuleOptionsOneList,
                SomeOnesCleanRuleOneList = _spaceService.ReadAnySpace(spaceId).SomeOnesCleanRuleOneList,
                CleanRuleOptionsTwoList = _spaceService.ReadAnySpace(spaceId).CleanRuleOptionsTwoList,
                SomeOnesCleanRuleTwoList = _spaceService.ReadAnySpace(spaceId).SomeOnesCleanRuleTwoList,
                CleanRuleOptionsThreeList = _spaceService.ReadAnySpace(spaceId).CleanRuleOptionsThreeList,
                SomeOnesCleanRuleThreeList = _spaceService.ReadAnySpace(spaceId).SomeOnesCleanRuleThreeList,
                CleanRuleOptionsFourList = _spaceService.ReadAnySpace(spaceId).CleanRuleOptionsFourList,
                SomeOnesCleanRuleFourList = _spaceService.ReadAnySpace(spaceId).SomeOnesCleanRuleFourList,
                SpaceoperatingDaysList = _spaceService.ReadAnySpace(spaceId).SpaceoperatingDaysList,
                _compareOperatingDay = _spaceService.ReadAnySpace(spaceId)._compareOperatingDay,
                Operating = _spaceService.Operating(),
                SpaceOwnerNameList = _spaceService.ReadAnySpace(spaceId).SpaceOwnerNameList,
            };
            return View(model);
        }
        /// <summary>
        /// 編輯場地(Steve) 
        /// </summary>
        /// <returns></returns>
        public ActionResult SpaceManage()
        {
            return View();
        }

        /// <summary>
        /// 場地主訂單 - 處理中(Steve)
        /// </summary>
        /// <returns></returns>
        public ActionResult Processing()
        {
            return View();
        }

        /// <summary>
        /// 場地主訂單 - 歷史訂單(Steve) --- 還沒做 @Nick
        /// </summary>
        /// <returns></returns>
        public ActionResult History()
        {
            return View();
        }
    }
}