using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoodFoodMKE.Startup))]
namespace GoodFoodMKE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
