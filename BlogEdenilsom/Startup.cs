using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogEdenilsom.Startup))]
namespace BlogEdenilsom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
