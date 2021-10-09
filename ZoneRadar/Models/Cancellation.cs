namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cancellation")]
    public partial class Cancellation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cancellation()
        {
            Space = new HashSet<Space>();
        }

        public int CancellationID { get; set; }

        [Required]
        [StringLength(20)]
        public string CancellationTitle { get; set; }

        [Required]
        [StringLength(300)]
        public string CancellationDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Space> Space { get; set; }
    }
}
