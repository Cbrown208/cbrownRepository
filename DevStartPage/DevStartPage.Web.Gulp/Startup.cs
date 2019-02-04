using DevStartPage.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace DevStartPage.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {}
    }
}
