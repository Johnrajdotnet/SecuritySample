using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuritySample.Infra.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AllowAnonymousAttribute : System.Attribute
    {
    }
}