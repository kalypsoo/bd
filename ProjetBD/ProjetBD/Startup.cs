using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjetBD.Startup))]
namespace ProjetBD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
