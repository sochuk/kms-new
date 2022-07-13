using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KMS.Context.VIEW
{
    [Table("NIKvsCARDUID")]
    public class NIKvsCARDUID
    {
        [Key]
        public string NIK { get; set; }
        public int TOTAL { get; set; }
    }
}