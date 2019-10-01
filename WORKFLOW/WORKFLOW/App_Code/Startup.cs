using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WORKFLOW.Startup))]
namespace WORKFLOW
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
