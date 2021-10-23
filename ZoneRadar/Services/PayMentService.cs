using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Services
{
    public static class PayMentService
    {
        public static decimal OrderDetailPrice(DateTime endDate, DateTime startDate, decimal hourPirce, int discountHour, decimal discount)
        {
            decimal HowMuchdiscount = 0;
            if ((int)endDate.Subtract(startDate).TotalHours >= discountHour)
            {
                HowMuchdiscount = discount;
            }
            else
            {
                HowMuchdiscount = 0;
            }
            var price = (decimal)endDate.Subtract(startDate).TotalHours * hourPirce * (1 - HowMuchdiscount);
            return price;
        }
    }
}