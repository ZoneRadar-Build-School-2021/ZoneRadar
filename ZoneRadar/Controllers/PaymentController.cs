﻿using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class PaymentController : Controller
    {
        private readonly EcpayMentService _ecpaymentservice;
        public PaymentController() 
        {
            _ecpaymentservice = new EcpayMentService();
        }
        // GET: Payment
        /// <summary>
        /// 綠界
        /// </summary>
        /// <returns></returns>
        public ActionResult Payment(CartsViewModel model)
        {
            return View();
        }

        public ActionResult EcPayment(CartsViewModel model) 
        {
            ViewData["EcPay"] = _ecpaymentservice.GetEcpayData(model);
            return View();
        }
        
    }
}