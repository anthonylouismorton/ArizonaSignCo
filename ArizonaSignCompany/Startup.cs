using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArizonaSignCompany.Startup))]
namespace ArizonaSignCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
