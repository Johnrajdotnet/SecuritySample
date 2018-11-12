using SecuritySample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SecuritySample.Infra
{
    public class Authenticate
    {

        public bool ValidateUser(LoginDetails logon)
        {
            // Check if this is a valid user.
            if (logon != null)
            {
                // Store the user temporarily in the context for this request.
                HttpContext.Current.Items.Add("OUser", logon);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateTicket(LoginDetails logon, HttpResponseBase response)
        {
            bool result = false;
            try
            {
                //Create the authentication ticket with custom user data.
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(logon);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        logon.UserId.ToString(),
                        DateTime.Now,
                        DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                        false,
                        userData,
                        FormsAuthentication.FormsCookiePath);
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);
                // Create the cookie.
                response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                result = true;
            }
            catch
            {

                throw;
            }
            return result;
        }
    }
}