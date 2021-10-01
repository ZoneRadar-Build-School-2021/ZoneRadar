namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CleaningProtocol")]
    public partial class CleaningProtocol
    {
        public int CleaningProtocolID { get; set; }

        public int SpaceID { get; set; }

        public int CleaningOptionID { get; set; }

        public virtual CleaningOption CleaningOption { get; set; }

        public virtual Space Space { get; set; }
    }
}
