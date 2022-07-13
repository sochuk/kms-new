using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KMS.Startup))]
namespace KMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}