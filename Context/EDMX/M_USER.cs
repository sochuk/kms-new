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
    
    public partial class M_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_USER()
        {
            this.LOG_USER = new HashSet<LOG_USER>();
            this.M_TOKEN = new HashSet<M_TOKEN>();
            this.MESSAGEs = new HashSet<MESSAGE>();
            this.MESSAGEs1 = new HashSet<MESSAGE>();
            this.NOTIFICATIONs = new HashSet<NOTIFICATION>();
        }
    
        public decimal USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string FULLNAME { get; set; }
        public string EMAIL { get; set; }
        public string TELEGRAM_ID { get; set; }
        public string PHONE { get; set; }
        public string PHOTO { get; set; }
        public Nullable<decimal> GENDER { get; set; }
        public Nullable<decimal> GROUP_ID { get; set; }
        public Nullable<decimal> ISACTIVE { get; set; }
        public Nullable<decimal> ISDELETE { get; set; }
        public Nullable<decimal> ISREQUIRED_TOKEN { get; set; }
        public Nullable<decimal> CREATEBY { get; set; }
        public Nullable<decimal> UPDATEBY { get; set; }
        public Nullable<decimal> DELETEBY { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public Nullable<System.DateTime> DELETEDATE { get; set; }
        public decimal THEME_ID { get; set; }
        public decimal COMPANY_ID { get; set; }
        public string NOTES { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<decimal> VENDOR_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_USER> LOG_USER { get; set; }
        public virtual M_COMPANY M_COMPANY { get; set; }
        public virtual M_GROUP M_GROUP { get; set; }
        public virtual M_SETTING M_SETTING { get; set; }
        public virtual M_THEME M_THEME { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_TOKEN> M_TOKEN { get; set; }
        public virtual M_VENDOR M_VENDOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE> MESSAGEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE> MESSAGEs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs { get; set; }
    }
}
