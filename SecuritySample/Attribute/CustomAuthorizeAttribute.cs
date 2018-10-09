using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SecuritySample.Attribute
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        bool isAuthorized = false;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
           
            //string redirect = string.Format("~/Authorize/UnAuthorizedView", HttpUtility.UrlEncode(httpContext.Request.RawUrl));
            var routeDataSet= httpContext.Request.RequestContext.RouteData;
            if (routeDataSet != null)//AccountManager.User != null
            {

                string controller = routeDataSet.Values["controller"] != null ? routeDataSet.Values["controller"].ToString() : string.Empty;
                string action = routeDataSet.Values["action"] != null ? routeDataSet.Values["action"].ToString() : string.Empty;
                IsAuthorized(controller, action);

            }

            return isAuthorized;
        }
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{

        //    if (!isAuthorized)
        //    {
        //        filterContext.Result = new HttpUnauthorizedResult();

        //    }

        //}

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
                    filterContext.Result = new RedirectResult("~/Account/Login");
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

        private class RedirectController : Controller
        {
            public ActionResult RedirectWhereever()
            {
                return RedirectToAction("Contact", "Home");
            }

        }

    }

}