namespace ZoneRadar.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Operating")]
    public partial class Operating
    {
        public int OperatingID { get; set; }

        public int SpaceID { get; set; }

        public int OperatingDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public virtual Space Space { get; set; }
    }
}
