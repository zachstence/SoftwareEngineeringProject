using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chicken.Web.Startup))]
namespace Chicken.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
