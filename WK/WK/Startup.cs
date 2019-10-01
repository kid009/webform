using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WK.Startup))]
namespace WK
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
