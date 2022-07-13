using DevExpress.Web.Bootstrap;
using KMS.Helper;
using KMS.Logs;
using KMS.Management.Model;
using KMS.Master.Model;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Web;

namespace KMS
{
    public partial class Dashboard : CPanel
    {
        public static DataTable vendor = new DataTable();
        public string seriesHourly = "";
        public string seriesDaily = "";
        public string seriesMonthly = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            if (vendorId > 0)
            {
                pnAverage.Visible = false;
                vendor = M_Vendor.SelectData(vendorId);
            }
            else
            {
                vendor = M_Vendor.SelectListWithContract();
            }

            foreach (DataRow row in vendor.Rows)
            {
                string vendorField = row["vendor_name"].ToString().ToLower().Replace(" ", string.Empty);
                string vendorName = row["vendor_name"].ToString();
                string vendorColor = row["color"].ToString();

                seriesHourly += "{";
                seriesHourly += "argumentField: 'times',";
                seriesHourly += "valueField: '" + vendorField + "',";
                seriesHourly += "name: '" + vendorName + "',";
                seriesHourly += "color: '" + vendorColor + "'";
                seriesHourly += "},";

                seriesDaily += "{";
                seriesDaily += "argumentField: 'day',";
                seriesDaily += "valueField: '" + vendorField + "',";
                seriesDaily += "name: '" + vendorName + "',";
                seriesDaily += "color: '" + vendorColor + "',";
                seriesDaily += "point: { visible: false }";
                seriesDaily += "},";

                seriesMonthly += "{";
                seriesMonthly += "argumentField: 'mon',";
                seriesMonthly += "valueField: '" + vendorField + "',";
                seriesMonthly += "name: '" + vendorName + "',";
                seriesMonthly += "color: '" + vendorColor + "',";
                seriesMonthly += "},";
            }

            //Temporary disabled
            //Task.Run(() => VendorLog.UpdateChart());
        }
    }
}