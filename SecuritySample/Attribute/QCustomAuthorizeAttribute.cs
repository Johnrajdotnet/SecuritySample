using JCustom.Attribute;

namespace SecuritySample.Attribute
{
    public class QCustomAuthorizeAttribute : JCustomAuthorizeAttribute
    {
       
        public QCustomAuthorizeAttribute(bool isUser,string reDirectUrl):base(isUser, reDirectUrl)
        {

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

        protected override bool IsUserAuthorizedHome(string action)
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