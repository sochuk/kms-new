namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.CARD_KAB")]
    public partial class CARD_KAB
    {
        [Key]
        public decimal KAB { get; set; }

        public decimal? VALUE { get; set; }

        [StringLength(100)]
        public string DESCRIPTION { get; set; }
    }
}
