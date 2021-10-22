using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Payment()
        {

            //AllInOne oPayment = new AllInOne();

            //List<string> enErrors = new List<string>();
            //try
            //{
            //    using (oPayment)
            //    {
            //        /* 服務參數 */
            //        oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
            //        oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
            //        oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
            //        oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
            //        oPayment.MerchantID = "2000132";//ECPay提供的特店編號


            //        /* 基本參數 */
            //        oPayment.Send.ReturnURL = "http://example.com";//付款完成通知回傳的網址
            //        oPayment.Send.ClientBackURL = "http://www.ecpay.com.tw/";//瀏覽器端返回的廠商網址
            //        oPayment.Send.OrderResultURL = "";//瀏覽器端回傳付款結果網址
            //        oPayment.Send.MerchantTradeNo = "ECPay" + new Random().Next(0, 99999).ToString();//廠商的交易編號
            //        oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間。
            //        oPayment.Send.TotalAmount = Decimal.Parse("50");//交易總金額
            //        oPayment.Send.TradeDesc = "交易描述";//交易描述
            //        oPayment.Send.ChoosePayment = PaymentMethod.ALL;//使用的付款方式
            //        oPayment.Send.Remark = "";//備註欄位
            //        oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
            //        oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
            //        oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
            //        oPayment.Send.IgnorePayment = "";//不顯示的付款方式
            //        //oPayment.Send.PlatformID = "";//特約合作平台商代號

            //        //訂單的商品資料
            //        oPayment.Send.Items.Add(new Item()
            //        {
            //            Name = "蘋果",//商品名稱
            //            Price = Decimal.Parse("50"),//商品單價
            //            Currency = "新台幣",//幣別單位
            //            Quantity = Int32.Parse("1"),//購買數量
            //            URL = "http://google.com",//商品的說明網址
            //            Unit = "顆",//商品單位
            //            TaxType = TaxationType.Taxable //商品課稅別
            //        });


            //        /*************************非即時性付款:ATM、CVS 額外功能參數**************/

            //        #region ATM 額外功能參數

            //        //oPayment.SendExtend.ExpireDate = 3;//允許繳費的有效天數
            //        //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
            //        //oPayment.SendExtend.ClientRedirectURL = "";//Client 端回傳付款相關資訊

            //        #endregion


            //        #region CVS 額外功能參數

            //        //oPayment.SendExtend.StoreExpireDate = 3; //超商繳費截止時間 CVS:以分鐘為單位 BARCODE:以天為單位
            //        //oPayment.SendExtend.Desc_1 = "test1";//交易描述 1
            //        //oPayment.SendExtend.Desc_2 = "test2";//交易描述 2
            //        //oPayment.SendExtend.Desc_3 = "test3";//交易描述 3
            //        //oPayment.SendExtend.Desc_4 = "";//交易描述 4
            //        //oPayment.SendExtend.PaymentInfoURL = "";//伺服器端回傳付款相關資訊
            //        //oPayment.SendExtend.ClientRedirectURL = "";///Client 端回傳付款相關資訊

            //        #endregion

            //        /***************************信用卡額外功能參數***************************/

            //        #region Credit 功能參數

            //        //oPayment.SendExtend.BindingCard = BindingCardType.No; //記憶卡號
            //        //oPayment.SendExtend.MerchantMemberID = ""; //記憶卡號識別碼
            //        //oPayment.SendExtend.Language = "ENG"; //語系設定

            //        #endregion Credit 功能參數

            //        #region 一次付清

            //        //oPayment.SendExtend.Redeem = false;   //是否使用紅利折抵
            //        //oPayment.SendExtend.UnionPay = true; //是否為銀聯卡交易

            //        #endregion

            //        #region 分期付款

            //        oPayment.SendExtend.CreditInstallment = "3,6";//刷卡分期期數

            //        #endregion 分期付款

            //        #region 定期定額

            //        //oPayment.SendExtend.PeriodAmount = 1000;//每次授權金額
            //        //oPayment.SendExtend.PeriodType = PeriodType.Day;//週期種類
            //        //oPayment.SendExtend.Frequency = 1;//執行頻率
            //        //oPayment.SendExtend.ExecTimes = 2;//執行次數
            //        //oPayment.SendExtend.PeriodReturnURL = "";//伺服器端回傳定期定額的執行結果網址。

            //        #endregion


            //        /********************* 電子發票開立延伸參數 ********************************/
            //        oPayment.Send.InvoiceMark = InvoiceState.Yes; // 指定要開立電子發票
            //        oPayment.SendExtend.RelateNumber = "ECPay" + new Random().Next(0, 99999).ToString();//廠商自訂編號                   
            //        oPayment.SendExtend.CustomerID = "A12345678";//客戶代號
            //        oPayment.SendExtend.CustomerIdentifier = "";//統一編號
            //        oPayment.SendExtend.CustomerName = "商家自訂名稱測試長度商家自訂名稱測試長度商家自訂名稱測試長度商家自訂名稱測試長度商家自訂名稱測試長度商家自訂名稱測試長度";//客戶名稱
            //        oPayment.SendExtend.CustomerAddr = "客戶地址";//客戶地址
            //        oPayment.SendExtend.CustomerPhone = "0912345678";//客戶手機號碼
            //        oPayment.SendExtend.CustomerEmail = "test1234560@gmail.com";//客戶電子郵件
            //        oPayment.SendExtend.ClearanceMark = CustomsClearance.None;//通關方式
            //        oPayment.SendExtend.TaxType = TaxationType.Taxable;//課稅類別
            //        oPayment.SendExtend.CarruerType = InvoiceVehicleType.Member;//載具類別
            //        oPayment.SendExtend.CarruerNum = "";//載具編號
            //        oPayment.SendExtend.Donation = DonatedInvoice.No;//捐贈註記
            //        oPayment.SendExtend.LoveCode = "";//愛心碼
            //        oPayment.SendExtend.Print = PrintFlag.No;//列印註記
            //        oPayment.SendExtend.InvoiceRemark = "";//備註
            //        oPayment.SendExtend.DelayDay = Int32.Parse("0");//延遲開立天數
            //        oPayment.SendExtend.InvType = TheWordType.General;//字軌類別


            //        /* 產生訂單 */
            //        enErrors.AddRange(oPayment.CheckOut());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // 例外錯誤處理。
            //    enErrors.Add(ex.Message);
            //}
            //finally
            //{
            //    // 顯示錯誤訊息。
            //    if (enErrors.Count() > 0)
            //    {
            //        // string szErrorMessage = String.Join("\\r\\n", enErrors);
            //    }
            //}
            //ViewBag.Ecpay = oPayment;
            var order = new Dictionary<string, string>
            {
            //特店交易編號
            { "MerchantTradeNo",  "ECPay" + new Random().Next(0, 99999).ToString()},

            //特店交易時間 yyyy/MM/dd HH:mm:ss
            { "MerchantTradeDate",  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")},

            //交易金額
            { "TotalAmount",  "100"},

            //交易描述
            { "TradeDesc",  "無"},

            //商品名稱
            { "ItemName",  "測試商品"},

            //允許繳費有效天數(付款方式為 ATM 時，需設定此值)
            { "ExpireDate",  "3"},

            //自訂名稱欄位1
            { "CustomField1",  ""},

            //自訂名稱欄位2
            { "CustomField2",  ""},

            //自訂名稱欄位3
            { "CustomField3",  ""},

            //自訂名稱欄位4
            { "CustomField4",  ""},

            //綠界回傳付款資訊的至 此URL
            { "ReturnURL",  "http://www.ecpay.com.tw/"},

            //付款完成通知回傳的網址
            { "ClientBackURL", "https://localhost:44322/UserCenter/Pending"},

            //使用者於綠界 付款完成後，綠界將會轉址至 此URL
            { "OrderResultURL", ""},

            //特店編號， 2000132 測試綠界編號
            { "MerchantID",  "2000132"},

            //交易類型 固定填入 aio
            { "PaymentType",  "aio"},

            //選擇預設付款方式 固定填入 ALL
            { "ChoosePayment",  "ALL"},

            //CheckMacValue 加密類型 固定填入 1 (SHA256)
            { "EncryptType",  "1"},
            };

            //檢查碼
            order["CheckMacValue"] = GetCheckMacValue(order);

            return View(order);
            //return View(oPayment);

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
    }
}