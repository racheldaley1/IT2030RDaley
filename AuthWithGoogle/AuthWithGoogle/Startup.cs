using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthWithGoogle.Startup))]
namespace AuthWithGoogle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
