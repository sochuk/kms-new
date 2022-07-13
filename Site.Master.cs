using System;
using System.Web.UI;
using KMS.Management.Model;

namespace KMS
{
    public partial class SiteMaster : MasterPage
    {        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                if (Context.User.Identity.IsAuthenticated)
                    this.Page.MasterPageFile = "~/Themes/"+ Context.User.Identity.Get_Theme() +"/" + Context.User.Identity.Get_Theme() + ".master";
                else
                    this.Page.MasterPageFile = "~/Site.master";

            }
            catch (Exception ex)
            {
                ex.Message.ToString();

                //Logout if theme doesnt exist
                Response.Redirect("~/account/logout");
            }
        }
    }
}