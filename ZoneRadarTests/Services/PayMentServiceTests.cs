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
        public void Given_EndTime_202111052000_StartTime_202111050900_HourPrice_120_DiscountHour_8_Discount_085_When_CalculateMoney_Then_1122()
        {
            decimal price = 1122;
            
        }
    }
}