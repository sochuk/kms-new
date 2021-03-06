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
    
    public partial class M_MODULE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_MODULE()
        {
            this.LOG_USER = new HashSet<LOG_USER>();
            this.M_ACCESS = new HashSet<M_ACCESS>();
            this.M_MODULE1 = new HashSet<M_MODULE>();
        }
    
        public decimal MODULE_ID { get; set; }
        public string TYPE_CODE { get; set; }
        public string MODULE_NAME { get; set; }
        public string MODULE_DESC { get; set; }
        public string MODULE_TITLE { get; set; }
        public Nullable<decimal> MODULE_ROOT { get; set; }
        public string MODULE_ICON { get; set; }
        public string MODULE_URL { get; set; }
        public Nullable<decimal> MODULE_TYPE { get; set; }
        public Nullable<decimal> ISACTIVE { get; set; }
        public Nullable<decimal> ISDELETE { get; set; }
        public Nullable<decimal> ISVISIBLE { get; set; }
        public Nullable<decimal> ORDER_NO { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public Nullable<System.DateTime> DELETEDATE { get; set; }
        public Nullable<decimal> CREATEBY { get; set; }
        public Nullable<decimal> UPDATEBY { get; set; }
        public Nullable<decimal> DELETEBY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_USER> LOG_USER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_ACCESS> M_ACCESS { get; set; }
        public virtual M_ICON M_ICON { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_MODULE> M_MODULE1 { get; set; }
        public virtual M_MODULE M_MODULE2 { get; set; }
    }
}
