using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SecuritySample.Attribute
{
    /// <summary>
    /// GenerateAntiforgeryTokenCookieForAjaxAttribute
    /// </summary>
    public class AntiforgeryTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string tokenHeader, tokenCookie, tokenHeader1;
            var request = filterContext.HttpContext.Request;
            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {
                try
                {
                    // get header token                    
                    tokenHeader1 = HttpContext.Current.Request.Headers.Get("__RequestVerificationToken");
                    tokenHeader = filterContext.HttpContext.Request.Headers.Get("__RequestVerificationToken");
                    // get cookie token
                    var requestCookie1 = HttpContext.Current.Request.Cookies["__AJAXAntiXsrfToken"];
                    var requestCookie = request.Cookies["__AJAXAntiXsrfToken"];
                    tokenCookie = requestCookie.Value;

                    AntiForgery.Validate(tokenCookie, tokenHeader);
                }
                catch
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.StatusCode = 403;
                    HttpContext.Current.Response.End();
                }
            }
        }
    }

    public class GlobalAntiForgeryTokenAttribute: FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {
                AntiForgery.Validate();
            }
        }
    }
}