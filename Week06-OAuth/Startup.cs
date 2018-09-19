using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week06_OAuth.Startup))]
namespace Week06_OAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
