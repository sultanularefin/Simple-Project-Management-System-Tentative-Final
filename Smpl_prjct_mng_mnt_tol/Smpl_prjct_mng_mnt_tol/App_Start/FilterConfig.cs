using System.Web;
using System.Web.Mvc;

namespace Smpl_prjct_mng_mnt_tol
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
