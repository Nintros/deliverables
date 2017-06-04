using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Deleverables.Web.Startup))]
namespace Deleverables.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
