//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KMS.Context.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class M_VENDOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_VENDOR()
        {
            this.LOG_PERSO = new HashSet<LOG_PERSO>();
            this.LOG_PERSO_COUNT = new HashSet<LOG_PERSO_COUNT>();
            this.M_CONTRACT = new HashSet<M_CONTRACT>();
            this.M_USER = new HashSet<M_USER>();
        }
    
        public decimal VENDOR_ID { get; set; }
        public string VENDOR_NAME { get; set; }
        public string VENDOR_DESC { get; set; }
        public Nullable<decimal> ISACTIVE { get; set; }
        public Nullable<decimal> ISDELETE { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public Nullable<System.DateTime> DELETEDATE { get; set; }
        public Nullable<decimal> CREATEBY { get; set; }
        public Nullable<decimal> UPDATEBY { get; set; }
        public Nullable<decimal> DELETEBY { get; set; }
        public Nullable<decimal> VENDOR_COLOR { get; set; }
        public string IP_ADDRESS { get; set; }
        public string PERSOSITE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_PERSO> LOG_PERSO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_PERSO_COUNT> LOG_PERSO_COUNT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_CONTRACT> M_CONTRACT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_USER> M_USER { get; set; }
    }
}
