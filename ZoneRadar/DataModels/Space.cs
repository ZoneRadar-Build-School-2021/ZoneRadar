namespace ZoneRadar.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Space")]
    public partial class Space
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Space()
        {
            Operating = new HashSet<Operating>();
            Orders = new HashSet<Orders>();
            SpaceAmenity = new HashSet<SpaceAmenity>();
            SpacePhoto = new HashSet<SpacePhoto>();
        }

        public int SpaceID { get; set; }

        public int MemberID { get; set; }

        [Required]
        [StringLength(50)]
        public string SpaceName { get; set; }

        [Required]
        public string Introduction { get; set; }

        public decimal MeasureOfArea { get; set; }

        public int Capacity { get; set; }

        public decimal PricePerHour { get; set; }

        public int MinHours { get; set; }

        [Required]
        public string Discount { get; set; }

        public int CountryID { get; set; }

        public int CityID { get; set; }

        public int DistrictID { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Latitude { get; set; }

        [Required]
        [StringLength(50)]
        public string Longitude { get; set; }

        [Required]
        public string CleaningProtocol { get; set; }

        [Required]
        public string HostRules { get; set; }

        [Required]
        public string Traffic { get; set; }

        [Required]
        public string ShootingEquipment { get; set; }

        [Required]
        [StringLength(100)]
        public string Cancellation { get; set; }

        [Required]
        public string Parking { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual City City { get; set; }

        public virtual Country Country { get; set; }

        public virtual District District { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operating> Operating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpaceAmenity> SpaceAmenity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpacePhoto> SpacePhoto { get; set; }
    }
}
