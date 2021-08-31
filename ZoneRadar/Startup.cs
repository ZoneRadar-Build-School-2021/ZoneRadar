using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZoneRadar.Startup))]
namespace ZoneRadar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
