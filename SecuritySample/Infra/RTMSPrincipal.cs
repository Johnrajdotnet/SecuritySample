
using RTMS.ViewModel.Account;
using SecuritySample.Models;
using System.Security.Principal;

namespace RTMS.ViewModel.Account
{
    public class RTMSPrincipal : IPrincipal
    {
        public RTMSPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public LoginDetails User { get; set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
