using SecuritySample.Infra;
using SecuritySample.Models;
using System;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Infra;

namespace SecuritySample.Controllers
{
    public class HomeController : Controller
    {
        private LoginDetails Loginn = new LoginDetails();
        private Authenticate authenticate = new Authenticate();
        //public ActionResult Index1()
        //{
        //    return View();
        //}
        [AllowAnonymous]
        [NoAntiForgery]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var logon = new LoginDetails();
            return View("~/Views/Login/LoginCRSFPage.cshtml", logon);
        }
        [HttpPost]
        [AllowAnonymous]
        //[ValidateInput(true)]
        public ActionResult CSRFLogin(LoginDetails logon)
        {
            //var Loginn = Login;
            Random random = new Random();

            if (ValidateUser(logon, Response))
            {
                // return RedirectToAction("About", "Home");
                return PartialView("~/Views/Shared/_HomeDashboard.cshtml");
                
            }
            else
            {
                ModelState.AddModelError("CustomError", "The user name or password provided is incorrect.");
                return View("~/Views/Login/LoginCRSFPage.cshtml");
            }

            //return RedirectToAction("About", "Home");

        }
        [HttpPost]
        public ActionResult Login(LoginDetails logon)
        {
            if (ValidateUser(logon, Response))
            {
                return RedirectToAction("DefaultHome", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", "The user name or password provided is incorrect.");
                return View("~/Views/Login/LoginCRSFPage.cshtml");
            }
        }
        private bool ValidateUser(LoginDetails logon, HttpResponseBase response)
        {

            bool result = false;
            //Temp Fix Login Issue
            // logon.UserId = Convert.ToInt32(logon.UserName);
            if (authenticate.ValidateUser(logon))

            {
                result = authenticate.AuthenticateTicket(logon, response);
            }

            return result;
        }
        [HttpPost]
        public ActionResult About(LoginDetails Loginn)
        {
            ViewBag.Message = "Your application description page.";
            //int i = 0;
            //int j = 5 / i;
            return View("~/Views/Home/About.cshtml");
        }
        [HttpPost]
        public ActionResult Contact(LoginDetails logon)
        {
            ViewBag.Message = "Your contact page.";
            return View("~/Views/Home/Contact.cshtml");
            //return View();
        }

    }
}