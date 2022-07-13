using KMS.Helper;
using KMS.Management.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace KMS.Master.Model
{
    public class StatusContract
    {
        public int status_id { get; set; }
        public string status_name { get; set; }
       
        public static DataTable SelectAllList()
        {
            List<StatusContract> data = new List<StatusContract>();
            data.Add(new StatusContract()
            {
                status_id = 0,
                status_name = "All"
            });
            data.Add(new StatusContract()
            {
                status_id = 1,
                status_name = "Active"
            });
            data.Add(new StatusContract()
            {
                status_id = 2,
                status_name = "In Active"
            });
            DataTable mdt = data.ToDataTable();

            return mdt;
        }
    }
}