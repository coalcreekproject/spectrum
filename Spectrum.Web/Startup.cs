using Microsoft.Owin;
using Owin;
using Spectrum.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Spectrum.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
