using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab_03.Startup))]
namespace Lab_03
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
