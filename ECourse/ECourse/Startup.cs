using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECourse.Startup))]
namespace ECourse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
