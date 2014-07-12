using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ynnova.YTrac.Web.Startup))]
namespace Ynnova.YTrac.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
