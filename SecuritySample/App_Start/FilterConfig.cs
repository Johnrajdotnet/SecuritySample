using JAuthorizeLibrary.Attribute;
using SecuritySample.Attribute;
using System.Web.Mvc;

namespace SecuritySample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new JErrorHandlerAttribute());
            filters.Add(new JLogonAuthorizeAtrribute());
            filters.Add(new QActionAuthorizeAttribute());
            filters.Add(new JAntiforgeryTokenAttribute());

        }
    }
}
