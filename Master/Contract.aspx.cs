using DevExpress.Web;
using KMS.Helper;
using KMS.Hubs;
using KMS.Management.Model;
using KMS.Master.Model;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace KMS.Master
{
    [NeedAccessRight]
    public partial class Contract : CPanel
    {
        public DataTable dt = new DataTable();
        public DataTable role = new DataTable();

        public DataTable dt_temp = new DataTable();
        string[] column_name = new string[] { "Contract Name", "Description" };
        string[] row_field = new string[] { "contract_name", "contract_desc" };

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();

            grid.bindDelete(grid_delete, dt_temp, column_name, row_field);

            grid.SettingsExport.FileName = Page.Title + " (" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")";

        }

        public void LoadData()
        {
            dt = M_Contract.SelectAll();

            (grid.Columns["vendor_id"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = M_Vendor.SelectAll();
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
            List<object> selected = grid.GetSelectedKey("contract_id");
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
                                M_Contract.Delete(new M_Contract { contract_id = id.ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.DELETE, "Delete contract", JObject.FromObject(new { contract_id = selected }), cnn, sqlTransaction);
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

            List<object> selected = grid.GetSelectedKey("contract_id");

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
                                M_Contract data = M_Contract.Select(item.ToString().ToInteger());
                                M_Contract.Enable(data, cnn, sqlTransaction);
                            }

                            Log.Insert(Log.LogType.ENABLE, "Enable contract", JObject.FromObject(new { contract_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                            grid.Refresh();
                            break;
                        case "BUTTONDISABLE":
                            foreach (var item in selected)
                            {
                                M_Contract.Disable(new M_Contract { contract_id = item.ToString().ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.DISABLE, "Disable contract", JObject.FromObject(new { contract_id = selected }), cnn, sqlTransaction);
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
                if (M_Contract.IsExist(new M_Contract { contract_name = e.NewValues["contract_name"].ToString() }))
                {
                    AddError(e.Errors, grid.Columns["contract_name"], "* Data exist");
                }
            }
            else
            {
                if (e.NewValues["contract_name"].ToString().Trim() != e.OldValues["contract_name"].ToString().Trim())
                {
                    if (M_Contract.IsExist(new M_Contract { contract_name = e.NewValues["contract_name"].ToString() }))
                    {
                        AddError(e.Errors, grid.Columns["contract_name"], "* Data exist");
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
                    ASPxGridLookup vendor = grid.FindID("vendor_id", "lookup_vendor");

                    M_Contract data = new M_Contract();
                    data.contract_name = (string)e.NewValues["contract_name"].ToNullString();
                    data.contract_desc = (string)e.NewValues["contract_desc"].ToNullString();
                    data.attachment = e.NewValues["attachment"].ToNullString();
                    data.period_start = Convert.ToDateTime(e.NewValues["period_start"].ToString());
                    data.period_end = Convert.ToDateTime(e.NewValues["period_end"].ToString());
                    data.quota = e.NewValues["quota"].ToInteger();                    
                    data.vendor_id = vendor.Value == null ? 0 : (int)vendor.Value;

                    try
                    {
                        data = M_Contract.Insert(data, cnn, sqlTransaction);
                        e.NewValues.Add(nameof(data.contract_id), data.contract_id);

                        Log.Insert(Log.LogType.ADD, "Add new contract", JObject.FromObject(e.NewValues), cnn, sqlTransaction);

                        sqlTransaction.Commit();
                        grid.alertSuccess();

                        var hub = GlobalHost.ConnectionManager.GetHubContext<VendorLogHub>();
                        hub.Clients.All.refreshDashboard();
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
                    ASPxGridLookup vendor = grid.FindID("vendor_id", "lookup_vendor");

                    M_Contract data = new M_Contract();
                    data.contract_id = e.Keys["contract_id"].ToInteger();
                    data.contract_name = (string)e.NewValues["contract_name"].ToNullString();
                    data.contract_desc = (string)e.NewValues["contract_desc"].ToNullString();
                    data.attachment = e.NewValues["attachment"].ToNullString();
                    data.period_start = Convert.ToDateTime(e.NewValues["period_start"].ToString());
                    data.period_end = Convert.ToDateTime(e.NewValues["period_end"].ToString());
                    data.quota = e.NewValues["quota"].ToInteger();
                    data.vendor_id = vendor.Value == null ? 0 : (int)vendor.Value;

                    try
                    {
                        M_Contract.Update(data, cnn, sqlTransaction);
                        e.NewValues.Add(nameof(data.contract_id), data.contract_id);
                        Log.Insert(Log.LogType.UPDATE, "Update contract", JObject.FromObject(e.NewValues), cnn, sqlTransaction);
                        sqlTransaction.Commit();
                        grid.alertSuccess();

                        var hub = GlobalHost.ConnectionManager.GetHubContext<VendorLogHub>();
                        hub.Clients.All.refreshDashboard();
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

        protected void lookup_vendor_Init(object sender, EventArgs e)
        {
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.DataSource = M_Vendor.SelectList();
            lookup.Bind("vendor_id");
        }
    }
}