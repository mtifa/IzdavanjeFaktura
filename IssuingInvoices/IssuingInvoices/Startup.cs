using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IssuingInvoices.Startup))]
namespace IssuingInvoices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
