using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NETDatingApp.Startup))]
namespace NETDatingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
