namespace KMS.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KMS.LOG_YEAR")]
    public partial class LOG_YEAR
    {
        [Key]
        public decimal FULLYEAR { get; set; }
    }
}
