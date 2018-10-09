//using Microsoft.AspNetCore.Antiforgery;
//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication2.Infra
{
    //[AttributeUsage(AttributeTargets.Class)]
    public class AntiForgeryAttribute : AuthorizeAttribute
    {
        //    //public void OnAuthorization(AuthorizationContext authorizationContext)
        //    //{
        //    //    if (authorizationContext.RequestContext.HttpContext.Request.HttpMethod != "POST")
        //    //        return;

        //    //    new ValidateAntiForgeryTokenAttribute().OnAuthorization(authorizationContext);
        //    //}

        //public void OnAuthorization(AuthorizationContext authorizationContext)
        //{
        //    if (authorizationContext.RequestContext.HttpContext.Request.HttpMethod != "POST")
        //        return;

        //    if (authorizationContext.ActionDescriptor.GetCustomAttributes(typeof(NoAntiForgeryCheckAttribute), true).Length > 0)
        //        return;

        //    new ValidateAntiForgeryTokenAttribute().OnAuthorization(authorizationContext);
        //}

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    var request = filterContext.HttpContext.Request;

        //    //  Only validate POSTs
        //    if (request.HttpMethod == WebRequestMethods.Http.Post)
        //    {
        //        //  Ajax POSTs and normal form posts have to be treated differently when it comes
        //        //  to validating the AntiForgeryToken
        //        if (request.IsAjaxRequest())
        //        {
        //            var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

        //            var cookieValue = antiForgeryCookie != null
        //                ? antiForgeryCookie.Value
        //                : null;

        //            AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
        //        }
        //        else
        //        {
        //            new ValidateAntiForgeryTokenAttribute()
        //                .OnAuthorization(filterContext);
        //        }
        //    }
        //}
        public override void OnAuthorization(AuthorizationContext filterContext)
        {


        }

    }

    public class GenerateAntiforgeryTokenCookieForAjaxAttribute : ActionFilterAttribute
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
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (context.HttpContext.Request.HttpMethod == WebRequestMethods.Http.Post)
            //{
            //    GetAntiXsrfToken();
            //}
        }

        public static string GetAntiXsrfToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            var responseCookie = new HttpCookie("__AJAXAntiXsrfToken")
            {
                HttpOnly = true,
                Value = cookieToken
            };
            if (FormsAuthentication.RequireSSL && HttpContext.Current.Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            HttpContext.Current.Response.Cookies.Set(responseCookie);

            return formToken;
        }

    }
    public class GlobalAntiForgeryTokenAttribute
  : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {
                AntiForgery.Validate();
            }
        }
    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    // public class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
    // {
    //     public const string HTTP_HEADER_NAME = "x-RequestVerificationToken";

    //     public override void OnAuthorization(AuthorizationContext filterContext)
    //     {
    //         var request = filterContext.HttpContext.Request;

    //         //  Only validate POSTs
    //         if (request.HttpMethod == WebRequestMethods.Http.Post)
    //         {

    //             var headerTokenValue = request.Headers[HTTP_HEADER_NAME];

    //             // Ajax POSTs using jquery have a header set that defines the token.
    //             // However using unobtrusive ajax the token is still submitted normally in the form.
    //             // if the header is present then use it, else fall back to processing the form like normal
    //             if (headerTokenValue != null)
    //             {
    //                 var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

    //                 var cookieValue = antiForgeryCookie != null
    //                     ? antiForgeryCookie.Value
    //                     : null;

    //                 AntiForgery.Validate(cookieValue, headerTokenValue);
    //             }
    //             else
    //             {
    //                 new ValidateAntiForgeryTokenAttribute()
    //                     .OnAuthorization(filterContext);
    //             }
    //         }
    //     }
    // }
}