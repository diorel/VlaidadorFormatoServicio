using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(clienteWCF.Startup))]
namespace clienteWCF
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
