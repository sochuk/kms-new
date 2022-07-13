namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.CARD")]
    public partial class CARD
    {
        [Key]
        public decimal LOG_ID { get; set; }

        [StringLength(20)]
        public string NIK { get; set; }

        [StringLength(25)]
        public string CARDUID { get; set; }

        [StringLength(120)]
        public string NAMA { get; set; }

        public byte? PROV { get; set; }

        public byte? KAB { get; set; }

        public DateTime? CREATEDATE { get; set; }
    }
}
