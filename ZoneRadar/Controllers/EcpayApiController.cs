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
        private readonly OrderService _orderservice;
        public EcpayApiController()
        {
            _ecpaymentservice = new EcpayMentService();
            _orderservice = new OrderService();
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

        [Route("api/historyapi/SearchHistoryList")]
        [HttpGet]
        public IHttpActionResult SearchHistoryList()
        {
            return Ok(_orderservice.GetHostCenterHistoryVM(1));
        }
    }
}
