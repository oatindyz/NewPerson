using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEB_PERSONAL.Startup))]
namespace WEB_PERSONAL
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
