using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;

[assembly: OwinStartup(typeof(SecuritySample.App_Start.Startup))]

namespace SecuritySample.App_Start
{
    public class Startup
    {
        public void ConfigureServices(ServiceCollection services)
        {
        }
        public void Configuration(IAppDomainSetup app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
