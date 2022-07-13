using DevExpress.Data.Filtering;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Web;
using KMS;
using KMS.Context;
using KMS.Helper;
using KMS.Hubs;
using KMS.Logs.Model;
using KMS.Management.Model;
using KMS.Master.Model;
using KMS.Notification;
using Microsoft.AspNet.SignalR;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CMS
{
    [NeedAccessRight]
    public partial class Data : CPanel
    {
        private static string lblprogress;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

            if (!IsPostBack && !IsCallback)
            {
                lblprogress = string.Empty;
            }
        }

        protected void grid_Init(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeGrid(false);
        }

        public void RebuildIndex(IHubContext hub, string username)
        {
            using (OracleConnection connection = new OracleConnection(Database.getConnectionString("Default")))
            {
                connection.Open();
                connection.CreateCommand();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    hub.Clients.User(username).started();
                    hub.Clients.User(username).getProgress("Please wait. Processing table log data for optimizing statistic...");

                    command.Parameters.Clear();
                    command.CommandText = "ANALYZE TABLE LOG_DAFDUK COMPUTE STATISTICS";
                    hub.Clients.User(username).started();
                    command.ExecuteNonQuery();

                    //hub.Clients.User(username).started();
                    //hub.Clients.User(username).getProgress("Please wait. Processing index data for optimizing statistic...");

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_CARDUID COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_CONTRACT COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_CREATEDATE COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_DATE COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_ERROR COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_PK COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    //command.CommandText = "ANALYZE INDEX LOG_PERSO_VENDOR COMPUTE STATISTICS";
                    //hub.Clients.User(username).started();
                    //command.ExecuteNonQuery();

                    Alert alert = new Alert("Success", "Data rebuild successfully", Alert.TypeMessage.Success);
                    hub.Clients.User(username).success(alert.ToString());

                    hub.Clients.User(username).finished();
                }
            }
        }

        private void ExportToXLS(IHubContext hub, string username)
        {
            hub.Clients.User(username).started();
            hub.Clients.User(username).getProgress("Please wait while loading data to export...");

            DevExpress.XtraPrinting.XlsExportOptionsEx options = new DevExpress.XtraPrinting.XlsExportOptionsEx();
            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            options.ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFilePageByPage;
            gridExporter.WriteXlsToResponse("Log Report - " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), true, options);

            hub.Clients.User(username).finished();
            Alert alert = new Alert("Success", "Data downloaded successfully", Alert.TypeMessage.Success);
            hub.Clients.User(username).success(alert.ToString());
        }

        private void ExportToXLSX(IHubContext hub, string username)
        {
            hub.Clients.User(username).started();
            hub.Clients.User(username).getProgress("Please wait while loading data to export...");

            DevExpress.XtraPrinting.XlsxExportOptionsEx options = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;
            gridExporter.WriteXlsxToResponse("Log Report - " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), true, options);

            hub.Clients.User(username).finished();
            Alert alert = new Alert("Success", "Data downloaded successfully", Alert.TypeMessage.Success);
            hub.Clients.User(username).success(alert.ToString());
        }

        protected void grid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeHtmlRowPrepared(e);
        }

        protected void grid_ToolbarItemClick(object source, DevExpress.Web.Data.ASPxGridViewToolbarItemClickEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)source;
            var hub = GlobalHost.ConnectionManager.GetHubContext<IISLogHub>();
            string username = M_User.getUsername();

            switch (e.Item.Name.ToUpper())
            {
                case "REBUILD":
                    //Task.Run(() => RebuildIndex(hub, username));
                    RebuildIndex(hub, username);
                    break;                

                case "XLSXEXPORT":
                    ExportToXLSX(hub, username);
                    break;

                default:
                    break;
            }

        }
        
        protected void VendorLogData_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "LOG_ID";
            var db = new KMSContext();
            var data = db.CARDs;
            e.QueryableSource = data;

            //var count = data.AsQueryable().Sum(s => s.TOTALTAKEN).ToInteger();
        }

        public string GenerateButton(string chartName)
        {
            string html = "<div class=\"text-center mb-3 mt-2\">";
            html += string.Format("<button class=\"mdc-button mdc-button--dense\" onclick=\"{0}.Print();return false;\"><i class=\"material-icons mdc-button__icon\">print</i> Print</button>", chartName);
            html += string.Format("<button class=\"mdc-button mdc-button--dense\" onclick=\"{0}.ExportTo('pdf');return false;\"><i class=\"material-icons mdc-button__icon\">get_app</i> PDF</button>", chartName);
            html += string.Format("<button class=\"mdc-button mdc-button--dense\" onclick=\"{0}.ExportTo('svg');return false;\"><i class=\"material-icons mdc-button__icon\">get_app</i> SVG</button>", chartName);
            html += string.Format("<button class=\"mdc-button mdc-button--dense\" onclick=\"{0}.ExportTo('png');return false;\"><i class=\"material-icons mdc-button__icon\">get_app</i> PNG</button>", chartName);
            html += string.Format("<button class=\"mdc-button mdc-button--dense\" onclick=\"{0}.ExportTo('jpeg');return false;\"><i class=\"material-icons mdc-button__icon\">get_app</i> JPEG</button>", chartName);
            html += string.Format("<div class=\"mdc-form-field\"><div class=\"mdc-checkbox mdc-checkbox--dark\"><input type=\"checkbox\" id=\"basic-disabled-checkbox\" class=\"mdc-checkbox__native-control\" onchange=\"isSelected(this, {0})\" /><div class=\"mdc-checkbox__background\"><svg class=\"mdc-checkbox__checkmark\" viewBox=\"0 0 24 24\"><path class=\"mdc-checkbox__checkmark-path\" fill=\"none\" d=\"M1.73,12.91 8.1,19.28 22.79,4.59\" /></svg><div class=\"mdc-checkbox__mixedmark\"></div></div></div><label for=\"basic-disabled-checkbox\" id=\"basic-disabled-checkbox-label\" class=\"m-0\">Show label</label></div>", chartName);
            html += "</div>";

            return html;
        }


        protected void CSVExport_Click(object sender, EventArgs e)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<IISLogHub>();
            string username = M_User.getUsername();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            hub.Clients.User(username).started();
            hub.Clients.User(username).getProgress("Please wait while loading data to export. This process take a few minutes ...");

            DevExpress.XtraPrinting.CsvExportOptionsEx options = new DevExpress.XtraPrinting.CsvExportOptionsEx();
            //gridExporter.WriteCsvToResponse("Log Report - " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), true, options);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "text/csv";
                Response.AddHeader("content-disposition", "attachment; filename=" + "Log Report - " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss") + ".csv");
                gridExporter.WriteCsv(memoryStream, options);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();

                hub.Clients.User(username).finished();
                Alert alert = new Alert("Success", "Data downloaded successfully in " + (stopwatch.ElapsedMilliseconds/1000) + " seconds", Alert.TypeMessage.Success);
                hub.Clients.User(username).success(alert.ToString());
                GC.Collect();
                
                Response.End();
            }
        }

        protected void ExcelExport_Click(object sender, EventArgs e)
        {
            ExportXLSX3();
        }


        private void ExportXLSX3()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<IISLogHub>();
            string username = M_User.getUsername();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            hub.Clients.User(username).started();
            hub.Clients.User(username).getProgress("Please wait while loading data to export. This process take a few minutes ...");

            var op = CriteriaOperator.Parse(grid.FilterExpression);
            var result = CriteriaToWhereClauseHelper.GetOracleWhere(op);

            var linqExpression = CriteriaToQueryableExtender.AppendWhere(new KMSContext().CARDs, new CriteriaToEFExpressionConverter(), op);

            int takeData = 1000000;
            int rowCount = grid.VisibleRowCount;
            double c = Convert.ToDouble(rowCount / 1000000.00);
            var divideSheet = Math.Ceiling(c);

            List<CARD> persos = new List<CARD>();

            int i = 0;
            int dataCount = linqExpression.Count();
            foreach (CARD item in linqExpression)
            {
                persos.Add(item);
                string percent = (Math.Round((i * .001f) / (dataCount * .001f) * 100.0f, 1)).ToString();
                hub.Clients.User(username).getProgress("Please wait while generating class data " + i + " of " + dataCount + " (" + percent + "% progress). Elapsed time : " + stopwatch.Elapsed);
                i++;
            }

            if (divideSheet == 1)
            {
                i = 1;

                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                //Header of table  
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "Card UID";
                workSheet.Cells[1, 2].Value = "NIK";
                workSheet.Cells[1, 3].Value = "Nama";
                workSheet.Cells[1, 4].Value = "Create Date";

                //Body of table  
                int recordIndex = 2;
                i = 1;
                foreach (var field in persos)
                {
                    try
                    {
                        string CARDUID = (!string.IsNullOrEmpty(field.CARDUID)) ? field.CARDUID : string.Empty;
                        string NIK = (!string.IsNullOrEmpty(field.NIK.ToString())) ? field.NIK.ToString() : string.Empty;
                        string NAMA = (!string.IsNullOrEmpty(field.NAMA)) ? field.NAMA : string.Empty;
                        string CREATEDATE = (field.CREATEDATE.HasValue) ? field.CREATEDATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;

                        workSheet.Cells[recordIndex, 1].Value = CARDUID;
                        workSheet.Cells[recordIndex, 2].Value = NIK;
                        workSheet.Cells[recordIndex, 3].Value = NAMA;
                        workSheet.Cells[recordIndex, 4].Value = CREATEDATE;

                        string percent = (Math.Round((i * .001f) / (persos.Count() * .001f) * 100.0f, 1)).ToString();
                        hub.Clients.User(username).getProgress("Generating excel data " + i + " of " + persos.Count() + "(" + percent + "% progress). Elapsed time : " + stopwatch.Elapsed);

                        CREATEDATE = null;
                        CARDUID = null;
                        NAMA = null;
                        CREATEDATE = null;
                    }
                    catch (Exception ex)
                    {
                        Log.Insert(Log.LogType.VIEW, "Error download excel", new { message = ex.Message.ToString() });
                    }

                    recordIndex++;
                    i++;
                }

                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();
                workSheet.Column(4).AutoFit();
                workSheet.Column(5).AutoFit();
                workSheet.Column(6).AutoFit();

                string excelName = "Log Perso - " + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    hub.Clients.User(username).getProgress("Please wait while finishing file and downloading data ...");
                    excel.SaveAsAsync(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                }

                Response.FlushAsync();

                hub.Clients.User(username).finished();
                Alert alert = new Alert("Success", "Data downloaded successfully in " + (stopwatch.ElapsedMilliseconds / 1000) + " seconds", Alert.TypeMessage.Success);
                hub.Clients.User(username).success(alert.ToString());

                persos.Clear();
                excel.Dispose();
                persos = null;
                excel = null;

                GC.Collect();

                Response.End();
            }
            else
            {
                ExcelPackage excel = new ExcelPackage();
                IEnumerable<CARD> itemFilter;

                for (int sheet = 1; sheet <= divideSheet; sheet++)
                {                    
                    if (sheet == 1)
                    {
                        itemFilter = persos.Take(takeData);
                    }
                    else
                    {
                        itemFilter = persos.Skip((sheet - 1) * takeData).Take(takeData);
                    }

                    var workSheet = excel.Workbook.Worksheets.Add("Sheet" + sheet);
                    workSheet.DefaultRowHeight = 12;

                    //Header of table  
                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Cells[1, 1].Value = "Card UID";
                    workSheet.Cells[1, 2].Value = "NIK";
                    workSheet.Cells[1, 3].Value = "Nama";
                    workSheet.Cells[1, 4].Value = "Create Date";

                    //Body of table  
                    int recordIndex = 2;
                    int filterCount = itemFilter.Count();
                    i = 0;
                    foreach (var field in itemFilter)
                    {
                        try
                        {
                            string CARDUID = (!string.IsNullOrEmpty(field.CARDUID)) ? field.CARDUID : string.Empty;
                            string NIK = (!string.IsNullOrEmpty(field.NIK.ToString())) ? field.NIK.ToString() : string.Empty;
                            string NAMA = (!string.IsNullOrEmpty(field.NAMA)) ? field.NAMA : string.Empty;
                            string CREATEDATE = (field.CREATEDATE.HasValue) ? field.CREATEDATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;

                            workSheet.Cells[recordIndex, 1].Value = CARDUID;
                            workSheet.Cells[recordIndex, 2].Value = NIK;
                            workSheet.Cells[recordIndex, 3].Value = NAMA;
                            workSheet.Cells[recordIndex, 4].Value = CREATEDATE;

                            string percent = (Math.Round((i * .001f) / (filterCount * .001f) * 100.0f, 1)).ToString();
                            hub.Clients.User(username).getProgress("Generating sheet data " + sheet + " of " + divideSheet + ". Row " + i + " of " + filterCount + " (" + percent + "% progress). Elapsed time : " + stopwatch.Elapsed);

                            CREATEDATE = null;
                            CARDUID = null;
                            NAMA = null;
                            CREATEDATE = null;
                        }
                        catch(Exception ex)
                        {
                            Log.Insert(Log.LogType.VIEW, "Error download excel", new { message = ex.Message.ToString() });
                        }

                        recordIndex++;
                        i++;
                    }

                    workSheet.Column(1).Width = 22;
                    workSheet.Column(2).Width = 20;
                    workSheet.Column(3).Width = 22;
                    workSheet.Column(4).Width = 14;
                    workSheet.Column(5).Width = 14;
                    workSheet.Column(6).Width = 30;

                }

                string excelName = "Log - " + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
                using (var memoryStream = new MemoryStream())
                {                    
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                    hub.Clients.User(username).getProgress("Please wait while finishing file and downloading data ...");
                    excel.SaveAsAsync(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);                    
                }


                Response.FlushAsync();

                hub.Clients.User(username).finished();
                Alert alert = new Alert("Success", "Data downloaded successfully in " + (stopwatch.ElapsedMilliseconds / 1000) + " seconds", Alert.TypeMessage.Success);
                hub.Clients.User(username).success(alert.ToString());

                persos.Clear();                
                excel.Dispose();
                persos = null;
                excel = null;
                itemFilter = null;

                GC.Collect();

                Response.End();

            }
        }
    }
}