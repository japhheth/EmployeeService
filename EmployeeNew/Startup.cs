using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeNew.Startup))]
namespace EmployeeNew
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
