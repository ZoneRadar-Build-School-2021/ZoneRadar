using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneRadar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneRadar.Services.Tests
{
    [TestClass()]
    public class PayMentServiceTests
    {
       [TestMethod()]
        public void Given_totaltime_11_HourPrice_120_DiscountHour_8_Discount_085_When_CalculateMoney_Then_1122()
        {
            //預期結果
            decimal expected = 1122;
            var totalhour = 11d;
            int discountHour = 8;
            var discount = 0.15;
            int hourmoney = 120;

            //tempdiscount
            double tempdiscount;
            if (totalhour >= discountHour)
            {
                tempdiscount = discount;
            }
            else
            {
                tempdiscount = 0;
            }
            var tempprice = (decimal)(totalhour * hourmoney * (1 - tempdiscount));
            //小數四捨五入
            var price = decimal.Round(tempprice);

            Assert.AreEqual(expected, price);
        }
    }
}