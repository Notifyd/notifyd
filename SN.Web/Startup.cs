using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Notifyd.Web.Startup))]
namespace Notifyd.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
