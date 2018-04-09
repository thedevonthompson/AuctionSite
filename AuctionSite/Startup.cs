using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuctionSite.Startup))]
namespace AuctionSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
