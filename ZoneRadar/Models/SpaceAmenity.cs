namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpaceAmenity")]
    public partial class SpaceAmenity
    {
        public int SpaceAmenityID { get; set; }

        public int SpaceID { get; set; }

        public int AmenityDetailID { get; set; }

        public virtual AmenityDetail AmenityDetail { get; set; }

        public virtual Space Space { get; set; }
    }
}
