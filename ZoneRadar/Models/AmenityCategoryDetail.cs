namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AmenityCategoryDetail")]
    public partial class AmenityCategoryDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AmenityCategoryDetail()
        {
            AmenityDetail = new HashSet<AmenityDetail>();
        }

        [Key]
        public int AmenityCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string AmenityCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AmenityDetail> AmenityDetail { get; set; }
    }
}
