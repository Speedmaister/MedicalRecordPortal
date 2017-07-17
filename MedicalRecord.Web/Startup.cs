using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalRecord.Web.Startup))]
namespace MedicalRecord.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
