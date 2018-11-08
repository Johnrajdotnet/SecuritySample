using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuritySample.Infra
{
    public class CustomActionRequest
    {
        public bool IsAuthorized(string controller, string action)
        {
            bool isAuthorized = false;
            switch (controller)
            {

                case "Home": { isAuthorized = IsUserAuthorizedHome(action); break; }
            }

            return isAuthorized;
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
    }
}