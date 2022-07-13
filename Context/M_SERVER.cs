namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.M_SERVER")]
    public partial class M_SERVER
    {
        [Key]
        public decimal SERVER_ID { get; set; }

        [StringLength(20)]
        public string SERVER_NAME { get; set; }

        [StringLength(200)]
        public string SERVER_DESC { get; set; }

        [StringLength(20)]
        public string IP_ADDRESS { get; set; }

        public decimal? ISACTIVE { get; set; }

        public decimal? ISDELETE { get; set; }

        public decimal? CREATEBY { get; set; }

        public decimal? UPDATEBY { get; set; }

        public decimal? DELETEBY { get; set; }

        public DateTime? CREATEDATE { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        public DateTime? DELETEDATE { get; set; }

        [StringLength(500)]
        public string LOG_PATH { get; set; }
    }
}
