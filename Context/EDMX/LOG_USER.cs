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
    
    public partial class LOG_USER
    {
        public decimal LOG_ID { get; set; }
        public Nullable<System.DateTime> LOG_DATE { get; set; }
        public string LOG_TITLE { get; set; }
        public Nullable<decimal> LOG_TYPE { get; set; }
        public Nullable<decimal> USER_ID { get; set; }
        public Nullable<decimal> MODULE_ID { get; set; }
        public string LOCAL_IP { get; set; }
        public string REMOTE_IP { get; set; }
        public string LOCATION { get; set; }
        public string BROWSER { get; set; }
        public string LOG_CONTENT { get; set; }
    
        public virtual M_MODULE M_MODULE { get; set; }
        public virtual M_USER M_USER { get; set; }
    }
}