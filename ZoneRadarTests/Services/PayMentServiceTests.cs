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
        [DataRow(8, 4, 25 , 399 , 2394)]
        [DataRow(12, 0, 0, 299, 3588)]
        [DataRow(5, 6, 30, 599, 2995)]
        [DataRow(12, 3, 70, 1588, 5717)]
        [DataRow(8, 2, 10, 1999, 14393)]
        [DataRow(3, 3, 20, 487, 1169)]
        [DataRow(8, 7, 25, 5269, 31614)]
        [DataRow(7, 9, 35, 5487, 38409)]
        [DataRow(12, 5, 90, 10000, 12000)]

        [TestMethod()]
         public void ComputeMoney(int totalhour , int discountHour , int discount , int hourmoney , int expected)
        //                           總共時數         最少折扣小時          折扣        一小時多少錢       計算金額
        {
            decimal tempdiscount;
            if (totalhour >= discountHour)
            {
                //              折扣 * 0.01m 轉成 decimal EX 20 => 0.2m
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