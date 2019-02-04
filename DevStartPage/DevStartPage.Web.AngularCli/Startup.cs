using DevStartPage.Web.AngularCli;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace DevStartPage.Web.AngularCli
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {}
    }
}
