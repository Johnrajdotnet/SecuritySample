using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebApplication2.Infra;

namespace System.Web.Mvc.Html
{
    public static class CustomHelpers
    {
        private static RijndaelManaged myRijndael = new RijndaelManaged();
        public static string RenderAsJson(this HtmlHelper helper, object model)
        {
            return Cryptography.EncryptStringAES(Json.Encode(model));
        }
        public static string SerializeToJsonString(object model)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return jsonString;
        }
        public static T DeSerializeJsonString<T>(string jsonString)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
            return obj;
        }
        public static string Encrypt(object model)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            byte[] strText = new System.Text.UTF8Encoding().GetBytes(jsonString);
            ICryptoTransform transform = myRijndael.CreateEncryptor();
            byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);

            return Convert.ToBase64String(cipherText);
        }
        public static string ConvertEncodeBase64String(object model)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            string encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            return encoded;
        }
        public static T ConvertDecodeBase64String<T>(string encodeBaseString)
        {
            byte[] data = Convert.FromBase64String(encodeBaseString);
            string decodedString = Encoding.UTF8.GetString(data);
            dynamic obj = Json.Decode(decodedString);
            return obj;
        }
        public static MvcForm BeginSecureForm(this HtmlHelper htmlHelper,
string actionName, string controllerName)
        {
            TagBuilder tagBuilder = new TagBuilder("form");

            tagBuilder.MergeAttribute("action",
            UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(),
            htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true));
            tagBuilder.MergeAttribute("method", "POST", true);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            var theForm = new MvcForm(htmlHelper.ViewContext);

            return theForm;
        }

        public static MvcHtmlString CustomAntiForgeryToken(this HtmlHelper htmlHelper)
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

            TagBuilder hidden = new TagBuilder("input");
            hidden.Attributes.Add("type", "hidden");
            hidden.Attributes.Add("name", "__RequestVerificationToken");
            if (formToken != null)
            {
                hidden.Attributes.Add("value", formToken.ToString());
            }
            return MvcHtmlString.Create(hidden.ToString());
        }
    }

}