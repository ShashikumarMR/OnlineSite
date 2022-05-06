using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineSiteModel.Startup))]
namespace OnlineSiteModel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
