using ECPay.Payment.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class EcpayMentService
    {
        private readonly ZONERadarRepository _repository;
        public EcpayMentService() 
        {
            _repository = new ZONERadarRepository();
        }

        /// <summary>
        /// 將收到的參數打包成綠界要形式 (Jack)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public String GetEcpayData(CartsViewModel model)
        {
            var Url = "https://zoneradar20211028194812.azurewebsites.net";
            AllInOne oPayment = new AllInOne();
            /* 服務參數 */
            oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
            oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
            oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
            oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
            oPayment.MerchantID = "2000132";//ECPay提供的特店編號

            /* 基本參數 */
            oPayment.Send.ReturnURL = Url + "/webapi/spaces/api/JSONAPI/GetEcpayData";//付款完成通知回傳的網址
            oPayment.Send.ClientBackURL = Url +"/UserCenter/Pending";//瀏覽器端返回的廠商網址
            oPayment.Send.OrderResultURL = "";//"http://localhost:53045/webapi/spaces/api/JSONAPI/GetEcpay";//瀏覽器端回傳付款結果網址
            oPayment.Send.MerchantTradeNo = "ZoneRadar" + new Random().Next(0, 9).ToString() + DateTime.Now.ToString("yyMMddHHmm");//廠商的交易編號
            oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間。
            oPayment.Send.TotalAmount = model.TotalMoney;//交易總金額
            oPayment.Send.TradeDesc = "交易描述";//交易描述
            oPayment.Send.ChoosePayment = PaymentMethod.Credit;//使用的付款方式
            oPayment.Send.Remark = "";//備註欄位
            oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
            oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.No;//是否需要額外的付款資訊
            oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
            oPayment.Send.IgnorePayment = "";//不顯示的付款方式
                                             //oPayment.Send.PlatformID = "";//特約合作平台商代號

            oPayment.Send.CustomField1 = model.OrderId.ToString();
            //oPayment.SendExtend.Desc_1 = "test1";//交易描述 1
            //oPayment.SendExtend.Desc_2 = "test2";//交易描述 2
            //oPayment.SendExtend.Desc_3 = "test3";//交易描述 3
            //oPayment.SendExtend.Desc_4 = "";//交易描述 4
                                            
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
            return html;
        }

        /// <summary>
        /// 修改訂單狀態 (Jack)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void EditOrderStatus(EcpayViewModel model) 
        {
            var order = _repository.GetAll<Order>().FirstOrDefault(x => x.OrderID == model.CustomField1);
            if (order != null)
            {
                order.PaymentDate = DateTime.Parse(model.PaymentDate);
                order.OrderStatusID = (int)Enums.Enums.OrderStatusID.OrderStatusIDforWating;
                order.OrderNumber = Get(int.Parse(DateTime.Now.ToString("yyMMddhhmm")));
                try
                {
                    _repository.Update(order);
                    _repository.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// 找OrderNumber 重複
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int Get(int num) 
        {
            var y = _repository.GetAll<Order>().FirstOrDefault(x=>x.OrderNumber == num );
            if (y != null) 
            {
                return Get(num++);
            }
            return num;
        }


        public PaymentViewModel GetPaymentData(int orderId)
        {
            var payments = new PaymentViewModel { RentDetail = new List<RentDetailViewModel>() };
            var Order = _repository.GetAll<Order>().Where(x=>x.OrderID == orderId).ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().Where(x => x.OrderID == orderId).ToList();
            foreach (var od in orderdetails)
            {
                var totalHousr = (int)od.EndDateTime.Subtract(od.StartDateTime).TotalHours;
                payments.RentDetail.Add(new RentDetailViewModel
                {
                    RentTime = od.StartDateTime.ToString(),
                    RentBackTime = od.EndDateTime.ToString(),
                    hours = totalHousr,
                    People = od.Participants
                });
            }

            var ruselt = (from o in Order
                          select new PaymentViewModel
                          {
                              SpaceId = o.Space.SpaceID,
                              UserName = o.Space.Member.Name,
                              UserPhoto = o.Space.Member.Photo,
                              PricePerHour = o.Space.PricePerHour,
                              RentDetail = payments.RentDetail,
                              CancellationTitle = o.Space.Cancellation.CancellationTitle,
                              CancellationDetail = o.Space.Cancellation.CancellationDetail,
                              Discount = o.Space.SpaceDiscount.FirstOrDefault().Discount,
                              Discounthours = o.Space.SpaceDiscount.FirstOrDefault().Hour
                          }).FirstOrDefault();
                          
            
            return ruselt;
        }
    }
}