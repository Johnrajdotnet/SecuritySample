using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecuritySample.Attribute
{
    public class IdeaAttribute
    {
        private class RedirectController : Controller
        {
            public ActionResult RedirectWhereever()
            {
                return RedirectToAction("Contact", "Home");
            }

        }
    }
    public static class AjaxRequest
    {

        public static bool IsAjaxRequest(this HttpRequestBase request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (request["X-Requested-With"] == "XMLHttpRequest")
                return true;
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}