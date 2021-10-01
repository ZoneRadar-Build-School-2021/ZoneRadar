namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CleaningCategory")]
    public partial class CleaningCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CleaningCategory()
        {
            CleaningOption = new HashSet<CleaningOption>();
        }

        public int CleaningCategoryID { get; set; }

        [Required]
        public string Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CleaningOption> CleaningOption { get; set; }
    }
}
