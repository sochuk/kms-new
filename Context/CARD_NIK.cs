namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.CARD_NIK")]
    public partial class CARD_NIK
    {
        [StringLength(20)]
        public string NIK { get; set; }

        public decimal? TOTAL { get; set; }

        public decimal ID { get; set; }
    }
}
