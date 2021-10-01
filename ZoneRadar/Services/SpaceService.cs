using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Data;
using ZoneRadar.Models;
using System.Web.Mvc;

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

        /// <summary>
        ///  便利設施 
        /// </summary>
        /// 
        /// 





        /// <summary>
        /// 取消政策 4種
        /// </summary>
        /// 


        /// <summary>
        /// 場地類型 17種 
        /// </summary>

        public List<SelectListItem> ShowSpaceType() 
        {
            var Spacetype = new List<SelectListItem>
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
            return Spacetype;
        }

        /// <summary>
        ///  停車資訊 6種
        /// </summary>
        public List<SelectListItem> ShowParking()
        {
            var parking = new List<SelectListItem>
            {
                new SelectListItem { Value = "免費現場停車", Text = "免費現場停車"},
                new SelectListItem { Value = "付費停車", Text = "付費停車" },
                new SelectListItem { Value = "免費路邊停車", Text = "免費路邊停車"},
                new SelectListItem { Value = "計時路邊停車", Text = "計時路邊停車"},
                new SelectListItem { Value = "代客停車", Text = "代客停車"},
                new SelectListItem { Value = "附近停車場", Text = "附近停車場"},

            };
            return parking;
        }
        /// <summary>
        ///  清潔 4大類
        ///  第一類
        /// </summary>
        public List<SelectListItem> ShowCleanFisrt()
        {
            var CleanFisrt = new List<SelectListItem>
            {
                new SelectListItem { Value = "根據當地衛生當局的指導方針對空間進行清潔和消毒", Text = "根據當地衛生當局的指導方針對空間進行清潔和消毒"},
                new SelectListItem { Value = "高接觸表面和共用設施已消毒", Text = "高接觸表面和共用設施已消毒" },
                new SelectListItem { Value = "柔軟的多孔材料已被正確清潔或移除", Text = "柔軟的多孔材料已被正確清潔或移除"},
                new SelectListItem { Value = "兩次預訂之間聘請專業清潔工", Text = "兩次預訂之間聘請專業清潔工"},
                new SelectListItem { Value = "預訂間隔開以加強清潔", Text = "預訂間隔開以加強清潔"},
            };
            return CleanFisrt;
        }
        /// <summary>
        ///  第二類
        /// </summary>
        public List<SelectListItem> ShowCleanSec()
        {
            var CleanSec = new List<SelectListItem>
            {
                new SelectListItem { Value = "消毒濕巾或噴霧劑和紙巾", Text = "消毒濕巾或噴霧劑和紙巾"},
                new SelectListItem { Value = "高接觸表面和共用設施已消毒", Text = "高接觸表面和共用設施已消毒" },
                new SelectListItem { Value = "一次性手套", Text = "一次性手套"},
                new SelectListItem { Value = "一次性口罩/面罩", Text = "一次性口罩/面罩"},
                new SelectListItem { Value = "消毒洗手液", Text = "消毒洗手液"},
            };
            return CleanSec;
        }
        /// <summary>
        ///  第三類
        /// </summary>
        public List<SelectListItem> ShowCleanThird()
        {
            var CleanThird = new List<SelectListItem>
            {
                new SelectListItem { Value = "空間可以使用室外空氣通風", Text = "空間可以使用室外空氣通風"},
                new SelectListItem { Value = "有空氣過濾器", Text = "有空氣過濾器" },
                new SelectListItem { Value = "空間有額外的戶外空間", Text = "空間有額外的戶外空間"},
                new SelectListItem { Value = "空間已重新配置以允許物理距離", Text = "空間已重新配置以允許物理距離"},
            };
            return CleanThird;
        }
        /// <summary>
        ///  第四類
        /// </summary>
        public List<SelectListItem> ShowCleanFourth()
        {
            var CleanFourth = new List<SelectListItem>
            {
                new SelectListItem { Value = "更新清潔程序的詳細清單", Text = "更新清潔程序的詳細清單"},
                new SelectListItem { Value = "訪客指南印出來", Text = "訪客指南印出來" },
                new SelectListItem { Value = "公共區域的地板上有1.5尺的標記", Text = "公共區域的地板上有1.5尺的標記"},
                new SelectListItem { Value = "狹窄的通道有雙向通行的箭頭", Text = "狹窄的通道有雙向通行的箭頭"},
            };
            return CleanFourth;
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
                new SelectListItem { Value = "24:00:00.0000000", Text = "24:00"},
            };
            return Operating;
        }
                
            
    }

}