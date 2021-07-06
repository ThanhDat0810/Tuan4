using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tuan4.Startup))]
namespace Tuan4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
