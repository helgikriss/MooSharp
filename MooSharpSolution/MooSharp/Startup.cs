using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MooSharp.Startup))]
namespace MooSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
