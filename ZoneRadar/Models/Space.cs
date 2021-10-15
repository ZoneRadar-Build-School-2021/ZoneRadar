namespace ZoneRadar.Models
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
            CleaningProtocol = new HashSet<CleaningProtocol>();
            Operating = new HashSet<Operating>();
            Order = new HashSet<Order>();
            SpaceAmenity = new HashSet<SpaceAmenity>();
            SpaceDiscount = new HashSet<SpaceDiscount>();
            SpacePhoto = new HashSet<SpacePhoto>();
            SpaceType = new HashSet<SpaceType>();
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
        public string HostRules { get; set; }

        [Required]
        public string Traffic { get; set; }

        public string Parking { get; set; }

        public string ShootingEquipment { get; set; }

        public int CancellationID { get; set; }

        public int CountryID { get; set; }

        public int CityID { get; set; }

        public int DistrictID { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public DateTime PublishTime { get; set; }

        [StringLength(50)]
        public string Latitude { get; set; }

        [StringLength(50)]
        public string Longitude { get; set; }

        public int SpaceStatusID { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public virtual Cancellation Cancellation { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CleaningProtocol> CleaningProtocol { get; set; }

        public virtual Country Country { get; set; }

        public virtual District District { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operating> Operating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        public virtual SpaceStatus SpaceStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpaceAmenity> SpaceAmenity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpaceDiscount> SpaceDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpacePhoto> SpacePhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpaceType> SpaceType { get; set; }
    }
}
