namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.LOG_PERSO_COUNT")]
    public partial class LOG_PERSO_COUNT
    {
        [Key]
        [Column(Order = 0)]
        public decimal VENDOR_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal CONTRACT_ID { get; set; }

        public decimal? CURRENT_HIT { get; set; }

        public decimal? SUCCESS_HIT { get; set; }

        public decimal? FAIL_HIT { get; set; }
    }
}
