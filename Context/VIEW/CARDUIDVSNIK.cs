using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KMS.Context.VIEW
{

    [Table("CARDUIDvsNIK")]
    public class CARDUIDvsNIK
    {
        [Key]
        public string CARDUID { get; set; }
        public int TOTAL { get; set; }
    }
}