using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventApplication.Startup))]
namespace EventApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
