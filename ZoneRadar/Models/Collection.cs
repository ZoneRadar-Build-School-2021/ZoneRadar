namespace ZoneRadar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Collection")]
    public partial class Collection
    {
        public int CollectionID { get; set; }

        public int MemberID { get; set; }

        public int SpaceID { get; set; }

        public virtual Member Member { get; set; }
    }
}
