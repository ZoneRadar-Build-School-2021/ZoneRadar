using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class EcpayViewModel
    {
        public string MerchantId { get; set; }
        public string MerchantTradeNo { get; set; }
        public string StoreId { get; set; }
        public int RtnCode { get; set; }
        public string RtnMsg { get; set; }
        public string TradeNo { get; set; }
        public int TradeAmt { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentTyoe { get; set; }
        public string TradeDate { get; set; }
        public int SimulatePaid { get; set; }
        public int CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CheckMacValue { get; set; }

    }
}