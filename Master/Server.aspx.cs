using DevExpress.Web;
using KMS.Helper;
using KMS.Management.Model;
using KMS.Master.Model;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMS.Master
{
    [NeedAccessRight]
    public partial class Server : CPanel
    {
        public DataTable dt_temp = new DataTable();
        string[] column_name = new string[] { "Server Name", "Description" };
        string[] row_field = new string[] { "server_name", "server_desc" };

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();

            grid.bindDelete(grid_delete, dt_temp, column_name, row_field);

            grid.SettingsExport.FileName = Page.Title + " (" + DateTime.Now.ToString("yyyyMMddHHmmss") + ")";
        }

        public void LoadData()
        {
            DataTable dt = M_Server.SelectAll();

            (grid.Columns["createby"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = M_User.BindData();
            (grid.Columns["updateby"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = M_User.BindData();

            grid.DataSource = dt;
            grid.DataBind();
        }

        protected void grid_Init(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeGrid();
        }

        protected void grid_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeCustomButton(e);
        }

        protected void grid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeCustomButtonCallback(e);

            List<object> selected = grid.GetSelectedKey("server_id");
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
                                M_Server.Enable(new M_Server { server_id = item.ToString().ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.ENABLE, "Enable server", JObject.FromObject(new { group_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                            grid.Refresh();
                            break;

                        case "BUTTONDISABLE":
                            foreach (var item in selected)
                            {
                                M_Server.Disable(new M_Server { server_id = item.ToString().ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.DISABLE, "Disable server", JObject.FromObject(new { group_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                            grid.Refresh();
                            break;
                    }

                }
            }
        }

        protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeCustomCallback(e);
            List<object> selected = grid.GetSelectedKey("server_id");
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
                                M_Server.Delete(new M_Server { server_id = id.ToInteger() }, cnn, sqlTransaction);
                            }
                            Log.Insert(Log.LogType.DELETE, "Delete server", JObject.FromObject(new { server_id = selected }), cnn, sqlTransaction);
                            sqlTransaction.Commit();
                        }
                        grid.JSProperties["cpShowDeleteConfirm"] = false;
                        grid.JSProperties["cpRefresh"] = true;
                        grid.alertSuccess("Data deleted successfully");
                    }
                    break;
            }
        }

        protected void grid_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (grid.IsNewRowEditing)
            {
                if (M_Server.IsExist("server_name", e.NewValues["server_name"].ToString() ))
                {
                    AddError(e.Errors, grid.Columns["server_name"], "* Data exist");
                }

                if (M_Server.IsExist("ip_address", e.NewValues["ip_address"].ToString()))
                {
                    AddError(e.Errors, grid.Columns["ip_address"], "* Data exist");
                }
            }
            else
            {
                if (e.NewValues["server_name"].ToString().Trim() != e.OldValues["server_name"].ToString().Trim())
                {
                    if (M_Server.IsExist("server_name", e.NewValues["server_name"].ToString()))
                    {
                        AddError(e.Errors, grid.Columns["server_name"], "* Data exist");
                    }
                }

                if (e.NewValues["ip_address"].ToString().Trim() != e.OldValues["ip_address"].ToString().Trim())
                {
                    if (M_Server.IsExist("ip_address", e.NewValues["ip_address"].ToString()))
                    {
                        AddError(e.Errors, grid.Columns["ip_address"], "* Data exist");
                    }
                }
            }
        }

        protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            using (var cnn = new OracleConnection(Database.getConnectionString("Default")))
            {
                cnn.Open();
                using (OracleTransaction sqlTransaction = cnn.BeginTransaction())
                {
                    M_Server data = new M_Server();
                    data.server_name = (string)e.NewValues["server_name"].ToNullString();
                    data.server_desc = (string)e.NewValues["server_desc"].ToNullString();
                    data.ip_address = (string)e.NewValues["ip_address"].ToNullString();
                    data.log_path = (string)e.NewValues["log_path"].ToNullString();

                    try
                    {
                        data = M_Server.Insert(data, cnn, sqlTransaction);
                        e.NewValues.Add(nameof(data.server_id), data.server_id);

                        Log.Insert(Log.LogType.ADD, "Add server", JObject.FromObject(e.NewValues), cnn, sqlTransaction);
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
            ASPxGridView grid = sender as ASPxGridView;
            using (var cnn = new OracleConnection(Database.getConnectionString("Default")))
            {
                cnn.Open();
                using (OracleTransaction sqlTransaction = cnn.BeginTransaction())
                {
                    M_Server data = new M_Server();
                    data.server_id = e.Keys["server_id"].ToInteger();
                    data.server_name = (string)e.NewValues["server_name"].ToNullString();
                    data.server_desc = (string)e.NewValues["server_desc"].ToNullString();
                    data.ip_address = (string)e.NewValues["ip_address"].ToNullString();
                    data.log_path = (string)e.NewValues["log_path"].ToNullString();

                    try
                    {
                        M_Server.Update(data, cnn, sqlTransaction);
                        e.NewValues.Add(nameof(data.server_id), data.server_id);

                        Log.Insert(Log.LogType.UPDATE, "Update server", JObject.FromObject(e.NewValues), cnn, sqlTransaction);
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
            ASPxGridView grid = sender as ASPxGridView;
            grid.ShowDeleteConfirm(e);
        }

        protected void grid_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeHtmlRowPrepared(e);
        }
    }
}