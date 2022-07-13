using KMS.Context;
using KMS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMS.CMS
{
    public partial class Overview : CPanel
    {
        public List<CARD_SUMMARY> summary = new List<CARD_SUMMARY>();
        public DataTable groupByProvince = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            summary = new List<CARD_SUMMARY>();
            using(var context = new KMSContext())
            {
                summary = (from a in context.CARD_SUMMARY
                          select a).ToList();

                groupByProvince = Database.getDataTable("SELECT * FROM CARD_PROV");

            }
        }
    }
}