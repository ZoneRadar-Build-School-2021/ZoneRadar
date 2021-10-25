using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;

namespace ZoneRadar.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        /// <summary>
        /// 綠界
        /// </summary>
        /// <returns></returns>
        public ActionResult Payment()
        {
            var ReturnURL = "http://www.ecpay.com.tw/";
            var ClientBackURL = "https://zoneradar20211025195223.azurewebsites.net/UserCenter/Pending";
            var MerchantTradeNo = "ECPay" + new Random().Next(0, 999).ToString() + DateTime.Now.ToString("yyyyMMddHHmm");
            var MerchantradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


            var order = new Dictionary<string, string>
            {
            //特店交易編號
            { "MerchantTradeNo", MerchantTradeNo },

            //特店交易時間 yyyy/MM/dd HH:mm:ss
            { "MerchantTradeDate",  MerchantradeDate},

            //交易金額
            { "TotalAmount",  "100"},

            //交易描述
            { "TradeDesc",  "無"},

            //商品名稱
            { "ItemName",  DateTime.Now.ToString()},

            //自訂名稱欄位1
            { "CustomField1",  ""},

            //自訂名稱欄位2
            { "CustomField2",  ""},

            //自訂名稱欄位3
            { "CustomField3",  ""},

            //自訂名稱欄位4
            { "CustomField4",  ""},

            //綠界回傳付款資訊的至 此URL
            { "ReturnURL", ReturnURL },

            //付款完成通知回傳的網址
            { "ClientBackURL", ClientBackURL },

            //使用者於綠界 付款完成後，綠界將會轉址至 此URL
            { "OrderResultURL", ""},

            //特店編號， 2000132 測試綠界編號
            { "MerchantID",  "2000132"},

            //交易類型 固定填入 aio
            { "PaymentType",  "aio"},

            //選擇預設付款方式 固定填入 ALL
            { "ChoosePayment",  "Credit"},

            //CheckMacValue 加密類型 固定填入 1 (SHA256)
            { "EncryptType",  "1"},
            };

            //檢查碼
            order["CheckMacValue"] = GetCheckMacValue(order);

            return View(order);
        }
        /// <summary>
        /// 取得 檢查碼
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string GetCheckMacValue(Dictionary<string, string> order)
        {
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();

            var checkValue = string.Join("&", param);

            //測試用的 HashKey
            var hashKey = "5294y06JbISpM5x9";

            //測試用的 HashIV
            var HashIV = "v77hoKGq4kWxNNIS";

            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";

            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();

            checkValue = GetSHA256(checkValue);

            return checkValue.ToUpper();
        }
        /// <summary>
        /// SHA256 編碼
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetSHA256(string value)
        {
            var result = new StringBuilder();
            var sha256 = SHA256Managed.Create();
            var bts = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bts);

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }


        public ActionResult EcPayment(OrderViewModel model) 
        {
           
            AllInOne oPayment = new AllInOne();
            
                /* 服務參數 */
                oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
                oPayment.MerchantID = "2000132";//ECPay提供的特店編號


                /* 基本參數 */
                oPayment.Send.ReturnURL = "http://example.com";//付款完成通知回傳的網址
                oPayment.Send.ClientBackURL = "http://www.ecpay.com.tw/";//瀏覽器端返回的廠商網址
                oPayment.Send.OrderResultURL = "";//瀏覽器端回傳付款結果網址
                oPayment.Send.MerchantTradeNo = "ZoneRadar" + new Random().Next(0, 9).ToString()+ DateTime.Now.ToString("yyMMddHHmm");//廠商的交易編號
                oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間。
                oPayment.Send.TotalAmount = Decimal.Parse("50");//交易總金額
                oPayment.Send.TradeDesc = "交易描述";//交易描述
                oPayment.Send.ChoosePayment = PaymentMethod.Credit;//使用的付款方式
                oPayment.Send.Remark = "";//備註欄位
                oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
                oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
                oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
                oPayment.Send.IgnorePayment = "";//不顯示的付款方式
                                                 //oPayment.Send.PlatformID = "";//特約合作平台商代號

                //訂單的商品資料
                oPayment.Send.Items.Add(new Item()
                {
                    Name = model.SpaceName,//商品名稱
                    Price = model.TotalMoney,//商品單價
                    Currency = "新台幣",//幣別單位
                    Quantity = Int32.Parse("1"),//購買數量
                    /*URL = "http://google.com",*///商品的說明網址
                    Unit = "個",//商品單位
                    TaxType = TaxationType.Taxable //商品課稅別
                });

                oPayment.SendExtend.CreditInstallment = "1";//刷卡分期期數

                var html = string.Empty;
                oPayment.CheckOutString(ref html);

                ViewData["EcPay"] = html;

            return View();
        }
    
        
    }
}