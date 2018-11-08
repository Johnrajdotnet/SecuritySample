using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JCustom.Attribute
{
    public class JCustomAuthorizeAttribute : AuthorizeAttribute
    {
        private bool isAuthorized = false;
        private bool isUserValid = false;
        private string loginUrl = string.Empty;
        protected JCustomAuthorizeAttribute(bool _isUserValid, string _redirectUrl)
        {
            isUserValid = _isUserValid;
            loginUrl = _redirectUrl;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeDataSet = httpContext.Request.RequestContext.RouteData;
            if (routeDataSet != null && isUserValid)
            {

                string controller = routeDataSet.Values["controller"] != null ? routeDataSet.Values["controller"].ToString() : string.Empty;
                string action = routeDataSet.Values["action"] != null ? routeDataSet.Values["action"].ToString() : string.Empty;
                isAuthorized= IsAuthorized(controller, action);

            }

            return isAuthorized;
        }
        /// <summary>
        /// using the client side we can redirect to any page
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!isAuthorized)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.HttpContext.Response.StatusDescription = "Humans and robots must authenticate";
                    filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    // check if a new session id was generated 
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
            }
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected virtual bool IsAuthorized(string controller, string action) {
            return IsUserAuthorizedHome(action);
        }
        protected virtual bool IsUserAuthorizedHome(string action)
        {
            return false;
        }

    }

}