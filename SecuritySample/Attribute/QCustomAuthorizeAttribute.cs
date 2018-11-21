using JAuthorizeLibrary.Attribute;

namespace SecuritySample.Attribute
{
    public class QActionAuthorizeAttribute : JActionAuthorizeAttribute
    {
        public QActionAuthorizeAttribute():base("~/Home/Index")
        {
                   
        }
        private static bool IsValidUser() {

            return true;
        }
        protected override bool IsAuthorized(string controller, string action)
        {
            bool isAuthorized = false;
            switch (controller)
            {

                case "Home": { isAuthorized = IsUserAuthorizedHome(action); break; }
            }

            return isAuthorized;
        }

        private bool IsUserAuthorizedHome(string action)
        {
            bool isAccessAction = false;
            switch (action)
            {

                case "DefaultHome": { isAccessAction = true; break; }
                case "Index":
                case "CSRFLogin":
                    { isAccessAction = true; break; }
                case "About":
                    { isAccessAction = true; break; }
                case "Contact":
                    { isAccessAction = true; break; }

            }
            return isAccessAction;
        }
    }
}