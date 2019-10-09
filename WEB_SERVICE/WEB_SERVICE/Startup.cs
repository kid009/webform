using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEB_SERVICE.Startup))]
namespace WEB_SERVICE
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
