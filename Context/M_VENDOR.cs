namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.M_VENDOR")]
    public partial class M_VENDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_VENDOR()
        {
            M_CONTRACT = new HashSet<M_CONTRACT>();
            M_USER = new HashSet<M_USER>();
        }

        [Key]
        public decimal VENDOR_ID { get; set; }

        [StringLength(20)]
        public string VENDOR_NAME { get; set; }

        [StringLength(200)]
        public string VENDOR_DESC { get; set; }

        public decimal? ISACTIVE { get; set; }

        public decimal? ISDELETE { get; set; }

        public DateTime? CREATEDATE { get; set; }

        public DateTime? UPDATEDATE { get; set; }

        public DateTime? DELETEDATE { get; set; }

        public decimal? CREATEBY { get; set; }

        public decimal? UPDATEBY { get; set; }

        public decimal? DELETEBY { get; set; }

        public decimal? VENDOR_COLOR { get; set; }

        [StringLength(20)]
        public string IP_ADDRESS { get; set; }

        [StringLength(250)]
        public string PERSOSITE { get; set; }

        public decimal? SERVER_ID { get; set; }

        [StringLength(20)]
        public string COLOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_CONTRACT> M_CONTRACT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_USER> M_USER { get; set; }
    }
}
