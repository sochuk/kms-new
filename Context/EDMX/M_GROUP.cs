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
    
    public partial class M_GROUP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public M_GROUP()
        {
            this.M_ACCESS = new HashSet<M_ACCESS>();
            this.M_USER = new HashSet<M_USER>();
        }
    
        public decimal GROUP_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public string GROUP_DESC { get; set; }
        public Nullable<decimal> ROLE_ID { get; set; }
        public Nullable<decimal> ISACTIVE { get; set; }
        public Nullable<decimal> ISDELETE { get; set; }
        public Nullable<System.DateTime> CREATEDATE { get; set; }
        public Nullable<System.DateTime> UPDATEDATE { get; set; }
        public Nullable<decimal> CREATEBY { get; set; }
        public Nullable<decimal> UPDATEBY { get; set; }
        public Nullable<decimal> DELETEBY { get; set; }
        public Nullable<System.DateTime> DELETEDATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_ACCESS> M_ACCESS { get; set; }
        public virtual M_ROLE M_ROLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<M_USER> M_USER { get; set; }
    }
}
