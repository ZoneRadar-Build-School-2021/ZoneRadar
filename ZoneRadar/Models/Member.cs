namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Collection = new HashSet<Collection>();
            Order = new HashSet<Order>();
            Space = new HashSet<Space>();
        }

        public int MemberID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Photo { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public string Description { get; set; }

        public bool ReceiveEDM { get; set; }

        public DateTime SignUpDateTime { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsVerify { get; set; }

        public int? GoogleLoginID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection> Collection { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Space> Space { get; set; }
    }
}
