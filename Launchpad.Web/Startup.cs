using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Launchpad.Startup))]
namespace Launchpad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
