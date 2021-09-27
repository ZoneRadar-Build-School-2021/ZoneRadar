namespace ZoneRadar.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpacePhoto")]
    public partial class SpacePhoto
    {
        public int SpacePhotoID { get; set; }

        public int SpaceID { get; set; }

        [Required]
        public string SpacePhotoUrl { get; set; }

        public virtual Space Space { get; set; }
    }
}
