namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.M_CONTRACT")]
    public partial class M_CONTRACT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_CONTRACT()
        {
            LOG_PERSO = new HashSet<LOG_PERSO>();
        }

        [Key]
        public decimal CONTRACT_ID { get; set; }

        [StringLength(50)]
        public string CONTRACT_NAME { get; set; }

        [StringLength(200)]
        public string CONTRACT_DESC { get; set; }

        [StringLength(2000)]
        public string ATTACHMENT { get; set; }

        public DateTime? PERIOD_START { get; set; }

        public DateTime? PERIOD_END { get; set; }

        public decimal? QUOTA { get; set; }

        public decimal? VENDOR_ID { get; set; }

        public decimal? ISACTIVE { get; set; }

        public decimal? ISDELETE { get; set; }

        public decimal? CREATEBY { get; set; }

        public decimal? UPDATEBY { get; set; }

        public decimal? DELETEBY { get; set; }

        public DateTime? CREATEDATE { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        public DateTime? DELETEDATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_PERSO> LOG_PERSO { get; set; }

        public virtual M_VENDOR M_VENDOR { get; set; }
    }
}
