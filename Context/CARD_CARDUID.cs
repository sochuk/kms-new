namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.CARD_CARDUID")]
    public partial class CARD_CARDUID
    {
        [StringLength(25)]
        public string CARDUID { get; set; }

        public decimal? TOTAL { get; set; }

        public decimal ID { get; set; }
    }
}
