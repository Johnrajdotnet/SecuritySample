using SecuritySample.Infra;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SecuritySample.Attribute
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private bool isAuthorized = false;
        private bool isUserValid = false;
        private string loginUrl = string.Empty;
        private CustomActionRequest customActionRequest = new CustomActionRequest();
        protected CustomAuthorizeAttribute(bool _isUserValid, string _redirectUrl)
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
                isAuthorized=customActionRequest.IsAuthorized(controller, action);

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

        public void IsAuthorized(string controller, string action)
        {
            switch (controller)
            {

                case "Home": { isAuthorized = IsUserAuthorizedHome(action); break; }
            }
        }

        public bool IsUserAuthorizedHome(string action)
        {
            bool isAccessAction = false;
            switch (action)
            {

                case "DefaultHome": { isAccessAction = true; break; }
                case "Index":
                case "CSRFLogin":
                    { isAccessAction = true; break; }
                case "About":
                    { isAccessAction = false; break; }

            }
            return isAccessAction;
        }
        #region referenc code
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
        //                             filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

        //    filterContext.HttpContext.Items["ActionDescriptor"] = filterContext.ActionDescriptor;

        //    // If the method did not exclusively opt-out of security (via the AllowAnonmousAttribute), then check for an authentication ticket.
        //    if (!skipAuthorization)
        //    {
        //        // CheckAuthorize(filterContext);
        //        base.OnAuthorization(filterContext);
        //    }
        //}
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var actionDescriptor = httpContext.Items["ActionDescriptor"] as ActionDescriptor;
        //    var routeDataSet = httpContext.Request.RequestContext.RouteData;
        //    if (routeDataSet != null)//AccountManager.User != null
        //    {

        //        string controller = routeDataSet.Values["controller"] != null ? routeDataSet.Values["controller"].ToString() : string.Empty;
        //        string action = routeDataSet.Values["action"] != null ? routeDataSet.Values["action"].ToString() : string.Empty;
        //        IsAuthorized(controller, action);

        //    }

        //    return isAuthorized;
        //}
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    if (!isAuthorized)
        //    {
        //        if (filterContext.HttpContext.Request.IsAjaxRequest())
        //        {
        //            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //            filterContext.HttpContext.Response.StatusDescription = "Action must authenticate";
        //            filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
        //            filterContext.HttpContext.Response.End();
        //        }
        //        else
        //        {
        //            // check if a new session id was generated 
        //            filterContext.Result = new RedirectResult("~/Account/Login");
        //        }
        //    }
        //    base.HandleUnauthorizedRequest(filterContext);
        //}
        #endregion

    }

}