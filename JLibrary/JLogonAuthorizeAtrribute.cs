using System.Web.Mvc;

namespace JLibrary.Attribute
{
    public sealed class JLogonAuthorizeAtrribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                                     filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            // If the method did not exclusively opt-out of security (via the AllowAnonmousAttribute), then check for an authentication ticket.
            if (!skipAuthorization)
            {
                base.OnAuthorization(filterContext);
                OnUnauthorizedResult(filterContext);
            }
        }

        private void OnUnauthorizedResult(AuthorizationContext filterContext)
        {
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
            }
        }
    }
}
