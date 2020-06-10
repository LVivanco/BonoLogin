using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BonoLogin.Startup))]
namespace BonoLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
