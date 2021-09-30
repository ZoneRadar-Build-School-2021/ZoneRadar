namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AmenityDetail")]
    public partial class AmenityDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AmenityDetail()
        {
            SpaceAmenity = new HashSet<SpaceAmenity>();
        }

        public int AmenityDetailID { get; set; }

        [Required]
        [StringLength(50)]
        public string Amenity { get; set; }

        public int? AmenityCategoryID { get; set; }

        public virtual AmenityCategoryDetail AmenityCategoryDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpaceAmenity> SpaceAmenity { get; set; }
    }
}
