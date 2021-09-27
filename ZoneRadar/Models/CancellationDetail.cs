namespace ZoneRadar.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CancellationDetail")]
    public partial class CancellationDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CancellationDetail()
        {
            Space = new HashSet<Space>();
        }

        [Key]
        public int CancellationID { get; set; }

        [Required]
        [StringLength(20)]
        public string CancellationTitle { get; set; }

        [Column("CancellationDetail")]
        [Required]
        [StringLength(300)]
        public string CancellationDetail1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Space> Space { get; set; }
    }
}
