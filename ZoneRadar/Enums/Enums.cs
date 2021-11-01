using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Enums
{
    public class Enums
    {
        public enum OrderStatusID
        {
            OrderStatusIDforCart = 1,
            OrderStatusIDforWating = 2,
            OrderStatusIDforUsing = 3,
            OrderStatusIDforFinal = 4,
            OrderStatusIDforCancel = 5
        };

        public enum CancellationOptions
        {
            VeryFlexible = 1,
            Flexible = 2,
            StandardThirtyDays = 3,
            StandardNintyDays = 4
        }
    }
}