using SecuritySample.Attribute;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Infra;

namespace SecuritySample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorHandlerAttribute());
            filters.Add(new AntiforgeryTokenAttribute());
            filters.Add(new CustomAuthorizeAttribute()); 
            //filters.Add(new GlobalAntiForgeryTokenAttribute());
        }
    }
}
