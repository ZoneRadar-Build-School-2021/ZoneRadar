namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CleaningOption")]
    public partial class CleaningOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CleaningOption()
        {
            CleaningProtocol = new HashSet<CleaningProtocol>();
        }

        public int CleaningOptionID { get; set; }

        [Required]
        public string OptionDetail { get; set; }

        public int CleaningCategoryID { get; set; }

        public virtual CleaningCategory CleaningCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CleaningProtocol> CleaningProtocol { get; set; }
    }
}
