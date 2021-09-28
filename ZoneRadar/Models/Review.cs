namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ReviewID { get; set; }

        public int OrderID { get; set; }

        public bool ToHost { get; set; }

        public int Score { get; set; }

        [Required]
        public string ReviewContent { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ReviewDate { get; set; }

        public bool Recommend { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
