using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Greenlight.Startup))]
namespace Greenlight
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
