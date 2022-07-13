using System.Web.Optimization;

namespace KMS.Themes.Azia
{
    public class Azia
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/themes/azia/css/iconpack").Include(
                            "~/Themes/material/css/icons/material-designicons.css",
                            "~/Themes/material/css/icons/flag-icon.css",
                            "~/Themes/material/css/icons/fontawesome.css",
                            "~/Themes/material/css/icons/simple-line-icons.css",
                            "~/Themes/material/css/icons/coreui-icons.css"
                            ));

            bundles.Add(new StyleBundle("~/themes/azia/css/light").Include(
                            "~/Themes/material/css/vendor.bundle.base.css",
                            "~/Themes/material/css/pace.min.css",
                            "~/Themes/azia/css/style.css",
                            "~/Content/css/dx.css",
                            "~/Themes/material/css/components/bootstrapselect.min.css",
                            "~/Themes/material/css/components/datetimepicker.min.css",
                            "~/Themes/material/css/components/fullcalendar.min.css",
                            "~/Themes/material/css/components/jquery.toast.css",
                            "~/Themes/material/css/components/jquery.notify.css",
                            "~/Themes/material/css/components/sweetalert.min.css",
                            "~/Themes/material/css/components/jquery.bar.rating.css"
                            ));
            
            bundles.Add(new ScriptBundle("~/themes/azia/js/preloader").Include(
                            "~/Themes/Azia/js/preloader.min.js"
                            ));

            bundles.Add(new ScriptBundle("~/themes/azia/js/vendorbundle").Include(
                            "~/Themes/material/js/vendor.bundle.base.js"
                            ));

            bundles.Add(new ScriptBundle("~/themes/azia/js/data").Include(
                            "~/Themes/material/js/popper.min.js",
                            "~/Themes/azia/js/bootstrap.bundle.min.js",
                            "~/Themes/azia/js/azia.js",
                            "~/Themes/material/js/pace.min.js",
                            "~/Content/js/jscookies.js",
                            "~/Content/js/dx.js",
                            "~/Themes/material/js/jquery.validate.min.js",
                            "~/Themes/material/js/jquery.validation.additional-methods.js",
                            "~/Themes/material/js/bootstrap-maxlength.min.js",
                            "~/Themes/material/js/jquery.toast.min.js",
                            "~/Themes/material/js/jquery.notify.min.js",
                            "~/Themes/material/js/sweetalert.min.js",
                            "~/Themes/material/js/avatar.text.js",
                            "~/Themes/material/js/jquery.pretty.date.js"
                            ));

            bundles.Add(new ScriptBundle("~/themes/azia/js/signalr").Include(
                            "~/Content/js/jquery.signalR-2.4.1.min.js",
                            "~/Themes/material/js/hub.js"
                            ));

            bundles.Add(new ScriptBundle("~/themes/azia/js/dxfunction").Include(
                            "~/Content/js/dx.function.js"
                            ));

            bundles.Add(new ScriptBundle("~/themes/azia/js/jquery").Include(
                            "~/Content/js/jquery-3.4.1.min.js"
                            ));


            //Login Region
            bundles.Add(new StyleBundle("~/themes/azia/css/login").Include(
                            "~/Themes/material/css/vendor.bundle.base.css",
                            "~/Themes/material/css/material-bootstrap.css",
                            "~/Themes/material/css/style.css",
                            "~/Content/css/dx.css"
                            ));


            bundles.Add(new ScriptBundle("~/themes/azia/js/login").Include(
                            "~/Themes/material/js/popper.min.js",
                            "~/Themes/material/js/material.js",
                            "~/Themes/material/js/material-bootstrap.min.js",
                            "~/Themes/material/js/jquery.toast.min.js",
                            "~/Themes/material/js/jquery.validate.min.js",
                            "~/Themes/material/js/jquery.validation.additional-methods.js",
                            "~/Themes/material/js/jquery.countdown.min.js",
                            "~/Themes/material/js/login.js",
                            "~/Themes/material/js/avatar.text.js",
                            "~/Themes/Material/js/sweetalert.min.js",
                            "~/Content/js/dx.js"
                            ));

        }
    }
}