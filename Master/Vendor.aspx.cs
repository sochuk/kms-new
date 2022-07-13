using DevExpress.Web;
using KMS.Helper;
using KMS.Management.Model;
using KMS.Master.Model;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace KMS.Master
{
    [NeedAccessRight]
    public partial class Vendor : CPanel
    {
        public DataTable dt = new DataTable();
        public DataTable role = new DataTable();

        public DataTable dt_temp = new DataTable();
        string[] column_name = new string[] { "Vendor Name", "Description" };
        string[] row_field = new string[] { "vendor_name", "vendor_desc" };

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();

            grid.bindDelete(grid_delete, dt_temp, column_name, row_field);

            grid.SettingsExport.FileName = Page.Title + " (" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")";
        }

        public void LoadData()
        {
            dt = M_Vendor.SelectAll();

            (grid.Columns["vendor_color"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = typeof(M_Vendor.Color).ToDataTable("vendor_color", "vendor_color_desc");
            (grid.Columns["server_id"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = M_Server.SelectAll();
            (grid.Columns["createby"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = M_User.BindData();
            (grid.Columns["updateby"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = M_User.BindData();

            grid.DataSource = dt;
            grid.DataBind();
        }

        protected void grid_Init(object sender, EventArgs e)
        {
            grid = (ASPxGridView)sender;
            grid.initializeGrid();
        }

        protected void grid_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            grid = (ASPxGridView)sender;
            grid.initializeCustomButton(e);
        }

        protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            grid = (ASPxGridView)sender;
            grid.initializeHtmlRowPrepared(e);
        }

        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grid = (ASPxGridView)sender;
            grid.initializeCustomCallback(e);
            List<object> selected = grid.GetSelectedKey("vendor_id");
            switch (e.Parameters.ToUpper())
            {
                case "DELETE":
                    using (var cnn = new OracleConnection(Database.getConnectionString("Default")))
                    {
                        cnn.Open();
                        using (OracleTransaction sqlTransaction = cnn.BeginTransaction())
                        {
                            foreach (var item in selected)
                            {
                                var id = item.ToString();
                                M_Vendor.Delete(new M_Vendor { vendor_id = id.ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.DELETE, "Delete vendor", JObject.FromObject(new { vendor_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                        }
                        grid.JSProperties["cpShowDeleteConfirm"] = false;
                        grid.JSProperties["cpRefresh"] = true;
                        grid.alertSuccess("Data deleted successfully");
                    }
                    break;
            }
        }

        protected void grid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            grid = (ASPxGridView)sender;
            grid.initializeCustomButtonCallback(e);

            List<object> selected = grid.GetSelectedKey("vendor_id");

            using (var cnn = new OracleConnection(Database.getConnectionString("Default")))
            {
                cnn.Open();
                using (OracleTransaction sqlTransaction = cnn.BeginTransaction())
                {
                    switch (e.ButtonID.ToUpper())
                    {
                        case "BUTTONENABLE":
                            foreach (var item in selected)
                            {
                                M_Vendor.Enable(new M_Vendor { vendor_id = item.ToString().ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.ENABLE, "Enable vendor", JObject.FromObject(new { vendor_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                            grid.Refresh();
                            break;
                        case "BUTTONDISABLE":
                            foreach (var item in selected)
                            {
                                M_Vendor.Disable(new M_Vendor { vendor_id = item.ToString().ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.DISABLE, "Disable vendor", JObject.FromObject(new { vendor_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                            grid.Refresh();
                            break;
                    }

                }
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            grid = (ASPxGridView)sender;
            if (grid.IsNewRowEditing)
            {
                if (M_Vendor.IsExist("vendor_name", e.NewValues["vendor_name"].ToString()))
                {
                    AddError(e.Errors, grid.Columns["vendor_name"], "* Data exist");
                }

                if (M_Vendor.IsExist("ip_address", e.NewValues["ip_address"].ToString()))
                {
                    AddError(e.Errors, grid.Columns["ip_address"], "* Data exist");
                }
            }
            else
            {
                if (e.NewValues["vendor_name"].ToString().Trim() != e.OldValues["vendor_name"].ToString().Trim())
                {
                    if (M_Vendor.IsExist("vendor_name", e.NewValues["vendor_name"].ToString()))
                    {
                        AddError(e.Errors, grid.Columns["vendor_name"], "* Data exist");
                    }
                }

                if (e.NewValues["ip_address"].ToString().Trim() != e.OldValues["ip_address"].ToString().Trim())
                {
                    if (M_Vendor.IsExist("ip_address", e.NewValues["ip_address"].ToString()))
                    {
                        AddError(e.Errors, grid.Columns["ip_address"], "* Data exist");
                    }
                }
            }
        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = dt;
        }

        protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            grid = (ASPxGridView)sender;
            using (var cnn = new OracleConnection(Database.getConnectionString("Default")))
            {
                cnn.Open();
                using (OracleTransaction sqlTransaction = cnn.BeginTransaction())
                {
                    ASPxGridLookup color = grid.FindID("vendor_color", "lookup_vendor_color");
                    ASPxGridLookup server = grid.FindID("server_id", "lookup_server");

                    M_Vendor data = new M_Vendor();
                    data.vendor_name = (string)e.NewValues["vendor_name"].ToNullString();
                    data.vendor_desc = (string)e.NewValues["vendor_desc"].ToNullString();
                    data.ip_address = e.NewValues["ip_address"].ToNullString();
                    data.persosite = (string)e.NewValues["persosite"].ToNullString();
                    data.server_id = server.Value.ToInteger();
                    data.color = (string)e.NewValues["color"].ToNullString();

                    try
                    {
                        data = M_Vendor.Insert(data, cnn, sqlTransaction);
                        e.NewValues.Add(nameof(data.vendor_id), data.vendor_id);

                        Log.Insert(Log.LogType.ADD, "Add new vendor", JObject.FromObject(e.NewValues), cnn, sqlTransaction);
                        sqlTransaction.Commit();
                        grid.alertSuccess();
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                        grid.alertError(ex.Message);
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message);
                    }

                    (sender as ASPxGridView).CancelEdit();
                    LoadData();
                }
                cnn.Close();
            }
            e.Cancel = true;
        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            grid = (ASPxGridView)sender;
            using (var cnn = new OracleConnection(Database.getConnectionString("Default")))
            {
                cnn.Open();
                using (OracleTransaction sqlTransaction = cnn.BeginTransaction())
                {
                    ASPxGridLookup color = grid.FindID("vendor_color", "lookup_vendor_color");
                    ASPxGridLookup server = grid.FindID("server_id", "lookup_server");

                    M_Vendor data = new M_Vendor();
                    data.vendor_name = (string)e.NewValues["vendor_name"].ToNullString();
                    data.vendor_desc = (string)e.NewValues["vendor_desc"].ToNullString();
                    data.ip_address = e.NewValues["ip_address"].ToNullString();
                    data.vendor_id = e.Keys["vendor_id"].ToInteger();
                    data.persosite = (string)e.NewValues["persosite"].ToNullString();
                    data.server_id = server.Value.ToInteger();
                    data.color = (string)e.NewValues["color"].ToNullString();

                    try
                    {
                        M_Vendor.Update(data, cnn, sqlTransaction);
                        e.NewValues.Add(nameof(data.vendor_id), data.vendor_id);
                        Log.Insert(Log.LogType.UPDATE, "Update vendor", JObject.FromObject(e.NewValues), cnn, sqlTransaction);
                        sqlTransaction.Commit();
                        grid.alertSuccess();
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                        grid.alertError(ex.Message.ToString());
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message);
                    }

                    (sender as ASPxGridView).CancelEdit();
                    LoadData();
                }
                cnn.Close();
            }
            e.Cancel = true;
        }

        protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            grid = sender as ASPxGridView;
            grid.ShowDeleteConfirm(e);
        }

        protected void lookup_vendor_color_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.DataSource = typeof(M_Vendor.Color).ToDataTable("vendor_color", "vendor_color_desc");
            lookup.Bind("vendor_color");
        }

        protected void lookup_server_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.DataSource = M_Server.SelectList();
            lookup.Bind("server_id");
        }
    }
}