using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthIndividualAccounts.Startup))]
namespace AuthIndividualAccounts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
