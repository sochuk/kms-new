using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using KMS.Helper;
using KMS.Logs;
using KMS.Management.Model;
using KMS.Master.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace KMS
{
    public partial class Default : CPanel
    {
        public static DataTable vendor = new DataTable();
        private DataTable vendors = new DataTable();
        private DataTable contracts = new DataTable();
        private DataTable statusContracts = new DataTable();
        List<object> vendor_id = new List<object>();

        public string seriesHourly = "";
        public string seriesDaily = "";
        public string seriesWeekly = "";
        public string seriesMonthly = "";
        public string seriesSemester= "";

        protected void Page_Load(object sender, EventArgs e)
        {         
            int vendorId = HttpContext.Current.User.Identity.Get_VendorID().ToInteger();
            vendors = M_Vendor.SelectAllList();
            contracts = M_Contract.SelectAllList();
            statusContracts = StatusContract.SelectAllList();

            lookup_vendorf.DataSource = vendors;
            lookup_vendorf.DataBind();

            lookup_contractf.DataSource = contracts;
            lookup_contractf.DataBind();

            lookup_statusf.DataSource = statusContracts;
            lookup_statusf.DataBind();

            if (vendorId > 0)
            {
                //pnAverage.Visible = false;
                //vendor = M_Vendor.SelectData(vendorId);
                Response.Redirect("~/dashboard");
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
                seriesHourly += "valueField: '"+ vendorField + "',";
                seriesHourly += "name: '"+ vendorName + "',";
                seriesHourly += "color: '"+ vendorColor + "'";
                seriesHourly += "},";

                seriesDaily += "{";
                seriesDaily += "argumentField: 'fulldate',";
                seriesDaily += "valueField: '" + vendorField + "',";
                seriesDaily += "name: '" + vendorName + "',";
                seriesDaily += "color: '" + vendorColor + "',";
                seriesDaily += "point: { visible: false }";
                seriesDaily += "},";

                seriesWeekly += "{";
                seriesWeekly += "argumentField: 'week',";
                seriesWeekly += "valueField: '" + vendorField + "',";
                seriesWeekly += "name: '" + vendorName + "',";
                seriesWeekly += "color: '" + vendorColor + "',";
                seriesWeekly += "point: { visible: false }";
                seriesWeekly += "},";

                seriesMonthly += "{";
                seriesMonthly += "argumentField: 'mon',";
                seriesMonthly += "valueField: '" + vendorField + "',";
                seriesMonthly += "name: '" + vendorName + "',";                
                seriesMonthly += "color: '" + vendorColor + "',";
                seriesMonthly += "},";

                seriesSemester += "{";
                seriesSemester += "argumentField: 'half_year',";
                seriesSemester += "valueField: '" + vendorField + "',";
                seriesSemester += "name: '" + vendorName + "',";
                seriesSemester += "color: '" + vendorColor + "',";
                seriesSemester += "point: { visible: false }";
                seriesSemester += "},";
            }

            //if (Request.QueryString["callback"] != null)
            //{
            //    string callBackSignature = Request.QueryString["callBack"];
            //    string jsFunction = string.Format("{0}({1});", callBackSignature, LoadChartDailyData(chartDaily));
            //    Response.ContentType = "text/javascript; charset=utf-8";
            //    Response.Write(jsFunction);
            //    Response.End();
            //}

            //LoadChartDailyData(chartDaily);
            //LoadChartMonthlyData(chartMonthly);


            //Temporary disable
            //Task.Run(() => VendorLog.UpdateChart());
        }
        
        public DataTable LoadChartDailyData(BootstrapChart chart)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            chart.SettingsLegend.VerticalAlignment = VerticalAlign.Bottom;
            chart.SettingsLegend.HorizontalAlignment = HorizontalAlign.Center;
            chart.SettingsLegend.ItemTextPosition = UIElementPosition.Top;
            chart.CssClasses.Title = "chartTitle";
            chart.TitleSettings.Text = "DAILY LOG  -  " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).ToUpper() + " " + year.ToString();

            int x = 0;
            string query = "SELECT A.*, ";

            DataTable selected_vendor = M_Vendor.SelectList();
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

                string vendor_name = m_vendor.vendor_name.Replace(" ", string.Empty);

                var bar = new BootstrapChartBarSeries();
                bar.ValueField = string.Format("{0}_TOTALTAKEN", vendor_name).ToLower();
                bar.Name = m_vendor.vendor_name;
                bar.Color = ColorTranslator.FromHtml(m_vendor.color);
                chart.SeriesCollection.Add(bar);
                chart.SettingsExport.FileName = Page.Title + " - " + year.ToString();

                if (x != selected_vendor.Rows.Count)
                {
                    query += string.Format(@"
                            COALESCE(COUNT({0}.LOG_ID), 0) {0}_TOTALTAKEN, ", vendor_name);
                }
                else
                {
                    query += string.Format(@"
                            COALESCE(COUNT({0}.LOG_ID), 0) {0}_TOTALTAKEN ", vendor_name);
                }
            }

            query += @"
                        FROM (
                        ";

            x = 0;
            DataTable dtdate = DateHelper.getDate(year, month);
            foreach (DataRow dd in dtdate.Rows)
            {
                x++;
                if (x != dtdate.Rows.Count)
                {
                    query += string.Format(@"
                            SELECT TO_DATE('{0}', 'YYYY-MM-DD') MONTH_DISPLAY, TO_CHAR(TO_DATE('{0}', 'YYYY-MM-DD'), 'DD') MONTH_VAL FROM DUAL 
                            UNION ALL 
                            ", Convert.ToDateTime(dd["date"].ToString()).ToString("yyyy-MM-dd"));
                }
                else
                {
                    query += string.Format(@"
                            SELECT TO_DATE('{0}', 'YYYY-MM-DD') MONTH_DISPLAY, TO_CHAR(TO_DATE('{0}', 'YYYY-MM-DD'), 'DD') MONTH_VAL FROM DUAL 
                            ", Convert.ToDateTime(dd["date"].ToString()).ToString("yyyy-MM-dd"));
                }
            }

            query += @"
                        ) A
                        ";

            x = 0;
            foreach (DataRow vv in selected_vendor.Rows)
            {
                string vendor_name = vv["vendor_name"].ToString().Replace(" ", string.Empty);
                string vendor_ip = vv["ip_address"].ToString();
                query += string.Format(@" 
                            LEFT JOIN LOG_VENDOR {0} ON TO_CHAR(A.MONTH_DISPLAY, 'YYYY-MM-DD')=TO_CHAR({0}.LOG_DATE, 'YYYY-MM-DD') AND {0}.LOCAL_IP='{1}' 
                            ", vendor_name, vendor_ip);
            }

            query += @" 
                        GROUP BY A.MONTH_DISPLAY, A.MONTH_VAL, 
                        ";

            x = 0;
            foreach (DataRow xx in selected_vendor.Rows)
            {
                x++;
                string vendor_name = xx["vendor_name"].ToString().Replace(" ", string.Empty);
                if (x != selected_vendor.Rows.Count)
                {
                    query += string.Format(@" 
                        TRUNC({0}.LOG_DATE, 'MM'), {0}.LOCAL_IP, ", vendor_name);
                }
                else
                {
                    query += string.Format(@" 
                        TRUNC({0}.LOG_DATE, 'MM'), {0}.LOCAL_IP ", vendor_name);
                }
            }

            query += @" 
                        ORDER BY A.MONTH_DISPLAY";

            DataTable dt = this.SelectChart(query, year);

            chart.SettingsCommonSeries.ArgumentField = "month_val";
            chart.SettingsCommonSeries.Label.Visible = false;
            chart.SettingsCommonSeries.Label.Format.Type = FormatType.FixedPoint;
            chart.SettingsCommonSeries.Label.Format.Precision = 0;
            chart.ArgumentAxis.DiscreteAxisDivisionMode = DiscreteAxisDivisionMode.CrossLabels;
            chart.ArgumentAxis.TickInterval = 1;
            chart.DataSource = dt;
            chart.DataBind();

            return dt;
        }

        protected void grid_Init(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeGrid(false);
        }

        public DataTable LoadChartMonthlyData(BootstrapChart chart)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            chart.SettingsLegend.VerticalAlignment = VerticalAlign.Bottom;
            chart.SettingsLegend.HorizontalAlignment = HorizontalAlign.Center;
            chart.SettingsLegend.ItemTextPosition = UIElementPosition.Top;
            chart.CssClasses.Title = "chartTitle";
            chart.TitleSettings.Text = "MONTHLY LOG  -  " + " YEAR " + year.ToString();

            int x = 0;
            string query = "SELECT A.*, ";

            DataTable selected_vendor = M_Vendor.SelectList();
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

                string vendor_name = m_vendor.vendor_name.Replace(" ", string.Empty);

                var bar = new BootstrapChartBarSeries();
                bar.ValueField = string.Format("{0}_TOTALTAKEN", vendor_name).ToLower();
                bar.Name = m_vendor.vendor_name;
                bar.Color = ColorTranslator.FromHtml(m_vendor.color);
                chart.SeriesCollection.Add(bar);
                chart.SettingsExport.FileName = Page.Title + " - " + year.ToString();

                if (x != selected_vendor.Rows.Count)
                {
                    query += string.Format(@"
                            COALESCE(COUNT({0}.LOG_ID), 0) {0}_TOTALTAKEN, ", vendor_name);
                }
                else
                {
                    query += string.Format(@"
                            COALESCE(COUNT({0}.LOG_ID), 0) {0}_TOTALTAKEN ", vendor_name);
                }
            }

            query += @"
                        FROM (
                        ";

            x = 0;
            DataTable dtdate = DateHelper.getDate(year, month);
            foreach (DataRow dd in dtdate.Rows)
            {
                x++;
                if (x != dtdate.Rows.Count)
                {
                    query += string.Format(@"
                            SELECT TO_DATE('{0}', 'YYYY-MM-DD') MONTH_DISPLAY, TO_CHAR(TO_DATE('{0}', 'YYYY-MM-DD'), 'DD') MONTH_VAL FROM DUAL 
                            UNION ALL 
                            ", Convert.ToDateTime(dd["date"].ToString()).ToString("yyyy-MM-dd"));
                }
                else
                {
                    query += string.Format(@"
                            SELECT TO_DATE('{0}', 'YYYY-MM-DD') MONTH_DISPLAY, TO_CHAR(TO_DATE('{0}', 'YYYY-MM-DD'), 'DD') MONTH_VAL FROM DUAL 
                            ", Convert.ToDateTime(dd["date"].ToString()).ToString("yyyy-MM-dd"));
                }
            }

            query += @"
                        ) A
                        ";

            x = 0;
            foreach (DataRow vv in selected_vendor.Rows)
            {
                string vendor_name = vv["vendor_name"].ToString().Replace(" ", string.Empty);
                string vendor_ip = vv["ip_address"].ToString();
                query += string.Format(@" 
                            LEFT JOIN LOG_VENDOR {0} ON TO_CHAR(A.MONTH_DISPLAY, 'YYYY-MM-DD')=TO_CHAR({0}.LOG_DATE, 'YYYY-MM-DD') AND {0}.LOCAL_IP='{1}' 
                            ", vendor_name, vendor_ip);
            }

            query += @" 
                        GROUP BY A.MONTH_DISPLAY, A.MONTH_VAL, 
                        ";

            x = 0;
            foreach (DataRow xx in selected_vendor.Rows)
            {
                x++;
                string vendor_name = xx["vendor_name"].ToString().Replace(" ", string.Empty);
                if (x != selected_vendor.Rows.Count)
                {
                    query += string.Format(@" 
                        TRUNC({0}.LOG_DATE, 'MM'), {0}.LOCAL_IP, ", vendor_name);
                }
                else
                {
                    query += string.Format(@" 
                        TRUNC({0}.LOG_DATE, 'MM'), {0}.LOCAL_IP ", vendor_name);
                }
            }

            query += @" 
                        ORDER BY A.MONTH_DISPLAY";

            DataTable dt = this.SelectChart(query, year);

            chart.SettingsCommonSeries.ArgumentField = "month_val";
            chart.SettingsCommonSeries.Label.Visible = false;
            chart.SettingsCommonSeries.Label.Format.Type = FormatType.FixedPoint;
            chart.SettingsCommonSeries.Label.Format.Precision = 0;
            chart.ArgumentAxis.DiscreteAxisDivisionMode = DiscreteAxisDivisionMode.CrossLabels;
            chart.ArgumentAxis.TickInterval = 1;
            chart.DataSource = dt;
            chart.DataBind();

            return dt;
        }

        public DataTable SelectChart(string query, int year)
        {
            DataTable dt = new DataTable();
            dt = Database.getDataTable(query,
                new OracleParameter(":YEAR", year));

            return dt;
        }

        public static DataTable SelectChart(string query)
        {
            DataTable dt = new DataTable();
            dt = Database.getDataTable(query);

            return dt;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //var startDate = Convert.ToDateTime(deStart.Date).ToString("dd/MM/yyyy");
            //var endDate = Convert.ToDateTime(deEnd.Date).ToString("dd/MM/yyyy");
            //vendor  = M_Vendor.SelectListWithContractByDateRange(startDate, endDate);
            Console.WriteLine(vendor);
        }

        protected void lookup_vendorf_Init(object sender, EventArgs e)
        {

        }

        protected void lookup_contractf_Init(object sender, EventArgs e)
        {

        }

        protected void lookup_statusf_Init(object sender, EventArgs e)
        {

        }
    }
}