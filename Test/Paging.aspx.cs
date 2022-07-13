using DevExpress.Web;
using KMS.Context;
using KMS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMS.Test
{
    public partial class Paging : CPanel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void IISLogData_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            var db = new KMSContext();
            e.KeyExpression = "LOG_ID";
            e.QueryableSource = db.LOG_PERSO;
        }

        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var parameter = e.Parameters;

        }

        protected void grid_PageIndexChanged(object sender, EventArgs e)
        {
            int pageIndex = grid.PageIndex;
            int pageCount = grid.PageCount;
            int pageSize = grid.SettingsPager.PageSize;
        }

        protected void grid_PageSizeChanged(object sender, EventArgs e)
        {
            int pageIndex = grid.PageIndex;
            int pageCount = grid.PageCount;
            int pageSize = grid.SettingsPager.PageSize;
        }

        protected void grid_Init(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.initializeGrid(false);

            int pageIndex = grid.PageIndex;
            int pageCount = grid.PageCount;
        }
    }
}