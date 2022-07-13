namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.LOG_IIS")]
    public partial class LOG_IIS
    {
        [Key]
        [Column(Order = 0)]
        public decimal LOG_ID { get; set; }

        public decimal? SCWIN32STATUS { get; set; }

        public decimal? SCSUBSTATUS { get; set; }

        public decimal? SCSTATUS { get; set; }

        [StringLength(500)]
        public string CSHOST { get; set; }

        [StringLength(500)]
        public string CSREFERER { get; set; }

        [StringLength(500)]
        public string CSCOOKIE { get; set; }

        [StringLength(500)]
        public string CSUSERAGENT { get; set; }

        [StringLength(500)]
        public string CSVERSION { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string CIP { get; set; }

        [StringLength(500)]
        public string CSUSERNAME { get; set; }

        public decimal? SPORT { get; set; }

        [StringLength(500)]
        public string CSURISTEM { get; set; }

        [StringLength(500)]
        public string CSURIQUERY { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string SIP { get; set; }

        [StringLength(200)]
        public string SCOMPUTERNAME { get; set; }

        [StringLength(200)]
        public string SSITENAME { get; set; }

        public DateTime? DATETIMEEVENT { get; set; }

        public decimal? CSBYTES { get; set; }

        public decimal? TIMETAKEN { get; set; }

        public decimal? TOTALTAKEN { get; set; }

        [StringLength(20)]
        public string CSMETHOD { get; set; }

        public DateTime? LOG_DATE { get; set; }

        [StringLength(20)]
        public string LOG_IPSERVER { get; set; }

        public decimal? LOG_YEAR { get; set; }

        public decimal? LOG_MONTH { get; set; }
    }
}
