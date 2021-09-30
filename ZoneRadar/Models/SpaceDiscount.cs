namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpaceDiscount")]
    public partial class SpaceDiscount
    {
        public int SpaceDiscountID { get; set; }

        public int SpaceID { get; set; }

        public int Hour { get; set; }

        public decimal Discount { get; set; }

        public virtual Space Space { get; set; }
    }
}
