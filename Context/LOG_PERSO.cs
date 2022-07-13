namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.LOG_PERSO")]
    public partial class LOG_PERSO
    {
        [Key]
        public decimal LOG_ID { get; set; }

        [StringLength(25)]
        public string CARDUID { get; set; }

        [StringLength(250)]
        public string CONTROLNUMBER { get; set; }

        [StringLength(250)]
        public string MANUFACTURERCODE { get; set; }

        [StringLength(250)]
        public string PERSOSITE { get; set; }

        public DateTime? PERSO_DATE { get; set; }

        [StringLength(250)]
        public string ERROR_CODE { get; set; }

        public decimal? VENDOR_ID { get; set; }

        public decimal? CONTRACT_ID { get; set; }

        public decimal? IS_COMMIT { get; set; }

        [StringLength(20)]
        public string IP_REQUEST { get; set; }

        public DateTime? CREATEDATE { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        [StringLength(20)]
        public string IP_ADDRESS { get; set; }

        public virtual M_CONTRACT M_CONTRACT { get; set; }
    }
}
