using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Topicos3.Startup))]
namespace Topicos3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
