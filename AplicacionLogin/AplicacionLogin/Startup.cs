using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AplicacionLogin.Startup))]
namespace AplicacionLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
