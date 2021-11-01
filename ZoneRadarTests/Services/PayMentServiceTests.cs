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
        [DataRow(11, 8, 15 , 120 , 1122)]
        [TestMethod()]
        public void ComputeMoney(int totalhour , int discountHour , int discount , int hourmoney , int expected)
        //                           總共時數         最少折扣小時          折扣        一小時多少錢       計算金額
        {
 
            decimal tempdiscount;
            if (totalhour >= discountHour)
            {
                tempdiscount = discount * 0.01m;
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