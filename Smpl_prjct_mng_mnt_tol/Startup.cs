using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smpl_prjct_mng_mnt_tol.Startup))]
namespace Smpl_prjct_mng_mnt_tol
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
