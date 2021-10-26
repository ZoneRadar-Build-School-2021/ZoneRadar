using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Services
{
    public static class PayMentService
    {
        public enum CancellationOptions
        {
            VeryFlexible = 1,
            Flexible = 2,
            StandardThirtyDays = 3,
            StandardNintyDays = 4
        }
        // 計算訂單價錢
        public static decimal OrderDetailPrice(DateTime endDate, DateTime startDate, decimal hourPirce, int discountHour, decimal discount)
        {

            decimal HowMuchdiscount = 0;
            if (endDate.Subtract(startDate).TotalHours >= discountHour)
            {
                HowMuchdiscount = discount;
            }
            else
            {
                HowMuchdiscount = 0;
            }
            //double 轉成 decimal
            var tempprice = (decimal)endDate.Subtract(startDate).TotalHours * hourPirce * (1 - HowMuchdiscount);
            //四捨五入
            var price = decimal.Round(tempprice);
            return price;
        }
        // 退款政策 + 退款金額
        public static decimal CancelPrice(int cancelid , double rentTimeToNow , decimal cancelmoney , decimal money)
        {
            //退錢判斷
            if (cancelid == (int)CancellationOptions.VeryFlexible)
            {
                if (rentTimeToNow <= 24)
                {
                    cancelmoney = 0;
                }
                else
                {
                    cancelmoney = money;
                }
            }
            else if (cancelid == (int)CancellationOptions.Flexible)
            {
                if (rentTimeToNow <= 24)
                {
                    cancelmoney = 0;
                }
                else if (rentTimeToNow > 24 && rentTimeToNow <= (24 * 7))
                {
                    cancelmoney = money / 2;
                }
                else
                {
                    cancelmoney = money;
                }
            }
            else if (cancelid == (int)CancellationOptions.StandardThirtyDays)
            {
                if (rentTimeToNow <= (24 * 7))
                {
                    cancelmoney = 0;
                }
                else if (rentTimeToNow > (24 * 7) && rentTimeToNow <= (24 * 30))
                {
                    cancelmoney = money / 2;
                }
                else
                {
                    cancelmoney = money;
                }
            }
            else if (cancelid == (int)CancellationOptions.StandardNintyDays)
            {
                if (rentTimeToNow <= (24 * 14))
                {
                    cancelmoney = 0;
                }
                else if (rentTimeToNow > (24 * 14) && rentTimeToNow <= (24 * 90))
                {
                    cancelmoney = money / 2;
                }
                else
                {
                    cancelmoney = money;
                }
            }
            return cancelmoney;
        }
    }
}