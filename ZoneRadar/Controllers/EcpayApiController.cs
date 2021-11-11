using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    [Authorize]
    public class EcpayApiController : ApiController
    {
        private readonly EcpayMentService _ecpaymentservice;
        public EcpayApiController() 
        {
            _ecpaymentservice = new EcpayMentService();
        }
        /// <summary>
        /// 回復綠界
        /// </summary>
        /// <param name="EcpayViewModel"></param>
        /// <returns></returns>
        [Route("api/EcpayAPI/GetEcpayData")]
        [HttpPost]
        public IHttpActionResult GetEcpayData(EcpayViewModel model)
        {
            if (model.RtnCode == 1)
            {
                _ecpaymentservice.EditOrderStatus(model);
            }
            return Ok("1|OK");
        }

        /// <summary>
        /// 綠界回復
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[Route("api/JSONAPI/GetEcpay")]
        //[HttpPost]
        //public IHttpActionResult GetEcpay(EcpayViewModel model)
        //{
        //    return Json(model);
        //}


        //[Route("api/historyapi/SearchHistoryList")]
        //[HttpPost]
        //public IHttpActionResult SearchHistoryList(HostCenterHistoryViewModel model)
        //{
        //    var a = model.SpaceName;
        //    var b = model.UserName;
        //    var c = model.SearchDateTime;
            

        //    return Ok("1|OK");
        //}
    }
}
