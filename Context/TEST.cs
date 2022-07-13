namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.TEST")]
    public partial class TEST
    {
        [Key]
        public DateTime TANGGAL { get; set; }

        public decimal? YEAR { get; set; }

        public decimal? MONTH { get; set; }
    }
}
