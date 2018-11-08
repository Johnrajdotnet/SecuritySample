using JCustom.Attribute;
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
            filters.Add(new JErrorHandlerAttribute());
            filters.Add(new JAntiforgeryTokenAttribute());
            filters.Add(new QActionAuthorizeAttribute(true, "~/Home/Index")); 
            //filters.Add(new GlobalAntiForgeryTokenAttribute());
        }
    }
}
