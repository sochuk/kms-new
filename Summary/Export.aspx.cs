using KMS.Helper;
using KMS.Management.Model;
using KMS.Master.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMS.Summary
{
    public partial class Export : CPanel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ExportExcel(DataTable dt)
        {
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            //Header of table  
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "LOG_DATE";
            workSheet.Cells[1, 2].Value = "VENDOR_ID";
            workSheet.Cells[1, 3].Value = "CONTRACT_ID";
            workSheet.Cells[1, 4].Value = "TOTAL";

            //Body of table  
            int recordIndex = 2;
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                workSheet.Cells[recordIndex, 1].Value = row["fulldate"].ToString();
                workSheet.Cells[recordIndex, 2].Value = row["vendor_id"].ToString();
                workSheet.Cells[recordIndex, 3].Value = row["contract_id"].ToString();
                workSheet.Cells[recordIndex, 4].Value = row["total"].ToString();

                recordIndex++;
                i++;
            }

            string excelName = "Log Perso Daily - " + DateTime.Now.ToString("yyyy-MM-dd");
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                excel.SaveAsAsync(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
            }

            Response.FlushAsync();
            excel.Dispose();

            Response.End();
        }

        private void ExportCSV(DataTable dt, string title)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
            sb.AppendLine(string.Join(";", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(";", fields));
            }

            string filename = "Log Perso Daily - " + DateTime.Now.ToString("yyyy-MM-dd");
            byte[] myByteArray = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            using (var memoryStream = new MemoryStream(myByteArray))
            {
                Response.ContentType = "text/csv";
                Response.AddHeader("content-disposition", "attachment; filename=" + title + ".csv");
                memoryStream.WriteTo(Response.OutputStream);
            }

            Response.FlushAsync();
            Response.End();
        }

        protected void btnHourlyExport_Click(object sender, EventArgs e)
        {
            int x = 0;
            int interval = 1;
            DataTable selected_vendor = new DataTable();
            DataTable result = new DataTable();

            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            if (vendorId > 0)
            {
                selected_vendor = M_Vendor.SelectListWithContract(vendorId);
            }
            else
            {
                selected_vendor = M_Vendor.SelectListWithContract();
            }

            x = 0;
            foreach (DataRow rvendor in selected_vendor.Rows)
            {
                x++;

                M_Vendor m_vendor = new M_Vendor()
                {
                    vendor_id = rvendor["vendor_id"].ToInteger(),
                    vendor_name = rvendor["vendor_name"].ToString(),
                    ip_address = rvendor["ip_address"].ToString(),
                    color = rvendor["color"].ToString(),
                    persosite = rvendor["persosite"].ToString(),
                };

                string vendor_name = m_vendor.vendor_name.Replace(" ", string.Empty);
                int contract_id = rvendor["contract_id"].ToInteger();

                string query = $@"
                WITH VENDOR AS(
                    SELECT PERSO_DATE,VENDOR_ID,CONTRACT_ID,CREATEDATE FROM LOG_PERSO WHERE TRUNC(CREATEDATE)=TRUNC(SYSDATE) 
                    AND ERROR_CODE='0' AND VENDOR_ID='{m_vendor.vendor_id}' AND CONTRACT_ID='{contract_id}'
                )
                ";

                var today = DateTime.Now.ToString("yyyy-MM-dd");
                query += $@"
                    SELECT A.*, '{m_vendor.vendor_id}' AS VENDOR_ID, '{contract_id}' AS CONTRACT_ID, COUNT(B.CREATEDATE) TOTAL
                    FROM (
                    SELECT TO_CHAR(TO_DATE('{today}', 'YYYY-MM-DD') + ( LEVEL - 1 ) * INTERVAL '{interval}' MINUTE, 'YYYY-MM-DD HH24:MI') AS FULLDATE,
                    TO_CHAR(TO_DATE('{today}', 'YYYY-MM-DD') + ( LEVEL - 1 ) * INTERVAL '{interval}' MINUTE, 'HH24:MI') AS TIMES
                    FROM DUAL
                    CONNECT BY TO_DATE('{today}', 'YYYY-MM-DD') + ( LEVEL - 1 ) * INTERVAL '{interval}' MINUTE < SYSDATE
                    )A
                    LEFT JOIN VENDOR B ON A.FULLDATE=TO_CHAR(TRUNC(B.CREATEDATE, 'MI'), 'YYYY-MM-DD HH24:MI') 
                    AND B.VENDOR_ID='{m_vendor.vendor_id}' 
                    AND B.CONTRACT_ID='{contract_id}'
                    GROUP BY A.FULLDATE, A.TIMES, TO_CHAR(TRUNC(B.CREATEDATE, 'MI'), 'YYYY-MM-DD HH24:MI'), B.VENDOR_ID, B.CONTRACT_ID
                    ORDER BY A.FULLDATE
                    ";

                DataTable dt = Database.getDataTable(query);
                if (result.Columns.Count == 0) result = dt.Clone();

                foreach(DataRow row in dt.Rows)
                {
                    result.ImportRow(row);
                }                
            }

            result.Columns.Remove("times");
            ExportCSV(result, "Log Hourly -" + DateTime.Now.ToString("dd MMMM yyyy"));
        }

        protected void btnDailyExport_Click(object sender, EventArgs e)
        {
            int x = 0;
            DataTable selected_vendor = new DataTable();
            DataTable result = new DataTable();

            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            if (vendorId > 0)
            {
                selected_vendor = M_Vendor.SelectListWithContract(vendorId);
            }
            else
            {
                selected_vendor = M_Vendor.SelectListWithContract();
            }

            x = 0;
            foreach (DataRow rvendor in selected_vendor.Rows)
            {
                x++;

                M_Vendor m_vendor = new M_Vendor()
                {
                    vendor_id = rvendor["vendor_id"].ToInteger(),
                    vendor_name = rvendor["vendor_name"].ToString(),
                    ip_address = rvendor["ip_address"].ToString(),
                    color = rvendor["color"].ToString(),
                };

                string vendor_name = m_vendor.vendor_name.Replace(" ", string.Empty).ToLower();
                string persosite = rvendor["persosite"].ToString();
                int contract_id = rvendor["contract_id"].ToInteger();

                string query = "";

                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                var days = DateTime.DaysInMonth(now.Year, now.Month);

                for (x = 0; x < days; x++)
                {
                    var d = startDate.AddDays(x).ToString("dd-MM-yyyy");
                    if (x == days - 1)
                    {

                        query += $@"SELECT TO_DATE('{d}', 'DD-MM-YYYY') AS FULLDATE,
                                '{m_vendor.vendor_id}' AS VENDOR_ID, 
                                '{contract_id}' AS CONTRACT_ID,
                                COUNT(*) AS TOTAL FROM LOG_PERSO 
                                WHERE TRUNC(CREATEDATE)=TRUNC(TO_DATE('{d}', 'DD-MM-YYYY')) 
                                AND VENDOR_ID={m_vendor.vendor_id} 
                                AND CONTRACT_ID={contract_id}
                                AND ERROR_CODE='0' ";
                    }
                    else
                    {
                        query += $@"SELECT TO_DATE('{d}', 'DD-MM-YYYY') AS FULLDATE, 
                                '{m_vendor.vendor_id}' AS VENDOR_ID, 
                                '{contract_id}' AS CONTRACT_ID,
                                COUNT(*) AS TOTAL FROM LOG_PERSO 
                                WHERE TRUNC(CREATEDATE)=TRUNC(TO_DATE('{d}', 'DD-MM-YYYY')) 
                                AND VENDOR_ID={m_vendor.vendor_id} 
                                AND CONTRACT_ID={contract_id}
                                AND ERROR_CODE='0' 
                                UNION ALL ";
                    }
                }

                DataTable dt = Database.getDataTable(query);
                
                if (result.Columns.Count == 0) result = dt.Clone();

                foreach (DataRow row in dt.Rows)
                {
                    result.ImportRow(row);
                }
            }

            foreach (DataColumn col in result.Columns) col.ReadOnly = false;
            result.Columns.Add("fulldates");
            foreach (DataRow row in result.Rows)
            {
                if (!string.IsNullOrEmpty(row["fulldate"].ToString()))
                {
                    row["fulldates"] = Convert.ToDateTime(row["fulldate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            result.Columns.Remove("fulldate");
            result.Columns["fulldates"].ColumnName = "fulldate";

            ExportCSV(result, "Log Daily -" + DateTime.Now.ToString("MMMM yyyy"));
        }

        protected void btnMonthlyExport_Click(object sender, EventArgs e)
        {
            int x = 0;
            DataTable selected_vendor = new DataTable();
            DataTable result = new DataTable();

            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            if (vendorId > 0)
            {
                selected_vendor = M_Vendor.SelectListWithContract(vendorId);
            }
            else
            {
                selected_vendor = M_Vendor.SelectListWithContract();
            }

            x = 0;
            foreach (DataRow rvendor in selected_vendor.Rows)
            {
                x++;

                M_Vendor m_vendor = new M_Vendor()
                {
                    vendor_id = rvendor["vendor_id"].ToInteger(),
                    vendor_name = rvendor["vendor_name"].ToString(),
                    ip_address = rvendor["ip_address"].ToString(),
                    color = rvendor["color"].ToString(),
                };

                string vendor_name = m_vendor.vendor_name.Replace(" ", string.Empty).ToLower();
                string persosite = rvendor["persosite"].ToString();
                int contract_id = rvendor["contract_id"].ToInteger();

                string query = "";

                var now = DateTime.Now;
                var startDate = new DateTime(now.Year, 1, 1);
                var endDate = startDate.AddMonths(12).AddDays(-1);

                for (x = 0; x < 12; x++)
                {
                    var d = startDate.AddMonths(x).ToString("dd-MM-yyyy");
                    var month = startDate.AddMonths(x).ToString("MMM");

                    if (x == 12 - 1)
                    {

                        query += $@"SELECT TO_DATE('{d}', 'DD-MM-YYYY') AS FULLDATE, 
                                '{month}' AS MON, 
                                '{m_vendor.vendor_id}' AS VENDOR_ID, 
                                '{contract_id}' AS CONTRACT_ID,
                                COUNT(*) AS TOTAL 
                                FROM LOG_PERSO 
                                WHERE TRUNC(CREATEDATE,'MM')=TRUNC(TO_DATE('{d}', 'DD-MM-YYYY'), 'MM') 
                                AND VENDOR_ID={m_vendor.vendor_id} 
                                AND CONTRACT_ID={contract_id}
                                AND ERROR_CODE='0' ";
                    }
                    else
                    {
                        query += $@"SELECT TO_DATE('{d}', 'DD-MM-YYYY') AS FULLDATE, 
                                '{month}' AS MON, 
                                '{m_vendor.vendor_id}' AS VENDOR_ID, 
                                '{contract_id}' AS CONTRACT_ID,                                
                                COUNT(*) AS TOTAL
                                FROM LOG_PERSO 
                                WHERE TRUNC(CREATEDATE,'MM')=TRUNC(TO_DATE('{d}', 'DD-MM-YYYY'), 'MM') 
                                AND VENDOR_ID={m_vendor.vendor_id} 
                                AND CONTRACT_ID={contract_id}
                                AND ERROR_CODE='0' 
                                UNION ALL ";
                    }
                }

                DataTable dt = Database.getDataTable(query);
                if (result.Columns.Count == 0) result = dt.Clone();

                foreach (DataRow row in dt.Rows)
                {
                    result.ImportRow(row);
                }
            }

            foreach (DataColumn col in result.Columns) col.ReadOnly = false;
            result.Columns.Add("fulldates");
            foreach (DataRow row in result.Rows)
            {
                if (!string.IsNullOrEmpty(row["fulldate"].ToString()))
                {
                    row["fulldates"] = Convert.ToDateTime(row["fulldate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            result.Columns.Remove("fulldate");
            result.Columns.Remove("mon");
            result.Columns["fulldates"].ColumnName = "fulldate";

            ExportCSV(result, "Log Monthly -" + DateTime.Now.ToString("MMMM yyyy"));

        }

        protected void btnExportVendor_Click(object sender, EventArgs e)
        {
            DataTable dt = M_Vendor.SelectAll();
            foreach(DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["createdate"].ToString()))
                {
                    row["createdate"] = Convert.ToDateTime(row["createdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                    
                if(!string.IsNullOrEmpty(row["updatedate"].ToString()))
                {
                    row["updatedate"] = Convert.ToDateTime(row["updatedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["deletedate"].ToString()))
                {
                    row["deletedate"] = Convert.ToDateTime(row["deletedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            ExportCSV(dt, "Master data vendor");
        }

        protected void btnExportContract_Click(object sender, EventArgs e)
        {
            DataTable dt = M_Contract.SelectAll();
            dt.Columns.Add("period_starts");
            dt.Columns.Add("period_ends");

            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["createdate"].ToString()))
                {
                    row["createdate"] = Convert.ToDateTime(row["createdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["updatedate"].ToString()))
                {
                    row["updatedate"] = Convert.ToDateTime(row["updatedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["deletedate"].ToString()))
                {
                    row["deletedate"] = Convert.ToDateTime(row["deletedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["period_start"].ToString()))
                {
                    row["period_starts"] = Convert.ToDateTime(row["period_start"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["period_end"].ToString()))
                {
                    row["period_ends"] = Convert.ToDateTime(row["period_end"]).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            dt.Columns.Remove("period_start");
            dt.Columns.Remove("period_end");
            dt.Columns.Remove("attachment");

            dt.Columns["period_starts"].ColumnName = "period_start";
            dt.Columns["period_ends"].ColumnName = "period_end";

            ExportCSV(dt, "Master data kontrak");
        }

        protected void btnExportServer_Click(object sender, EventArgs e)
        {
            DataTable dt = M_Server.SelectAll();
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["createdate"].ToString()))
                {
                    row["createdate"] = Convert.ToDateTime(row["createdate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["updatedate"].ToString()))
                {
                    row["updatedate"] = Convert.ToDateTime(row["updatedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["deletedate"].ToString()))
                {
                    row["deletedate"] = Convert.ToDateTime(row["deletedate"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (!string.IsNullOrEmpty(row["log_path"].ToString()))
                {
                    row["log_path"] = row["log_path"].ToString().Replace("\\", "\\\\");
                }
            }

            ExportCSV(dt, "Master data server");
        }
    }
}