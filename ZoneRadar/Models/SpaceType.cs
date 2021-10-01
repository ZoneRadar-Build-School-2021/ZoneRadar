namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpaceType")]
    public partial class SpaceType
    {
        [Key]
        public int TypeID { get; set; }

        public int SpaceID { get; set; }

        public int TypeDetailID { get; set; }

        public virtual Space Space { get; set; }

        public virtual TypeDetail TypeDetail { get; set; }
    }
}
