using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.Security;
using UpgradeIt.Model;

namespace UpgradeIt.Controllers
{
    public class MembershipController : SurfaceController
    {
        [HttpPost]
        public ActionResult Login(LoginModel loginData)
        {

            if (Membership.ValidateUser(loginData.Username, loginData.Password))
            {
                FormsAuthentication.SetAuthCookie(loginData.Username, false);
                TempData["LoginStatus"] = "Success!";
                return RedirectToCurrentUmbracoPage();

            }
            else
            {
                TempData["LoginStatus"] = "Invalid username or password";
                return RedirectToCurrentUmbracoPage();
            }
        }

        [HttpGet]
        public ActionResult Logout(string currentPageUrl)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect(currentPageUrl);
        }
        
        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewData)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterViewData"] = registerViewData;
                return CurrentUmbracoPage();
            }

            /*


            RegisterModel registerData = membershipHelper.CreateRegistrationModel();

            registerData.Email = registerViewData.Email;
            registerData.Password = registerViewData.Password;
            registerData.UsernameIsEmail = true;
            MembershipCreateStatus createStatus;
            membershipHelper.RegisterMember(registerData, out createStatus, false);
             */
            bool success = false;
            string errorMessage = "";

            if (!MemberExists(registerViewData.Email))
            {
                IMember newMember = ApplicationContext.Services.MemberService.CreateMember(registerViewData.Email, registerViewData.Email, registerViewData.Email, "Member");

                try
                {
                    ApplicationContext.Services.MemberService.Save(newMember);
                    ApplicationContext.Services.MemberService.SavePassword(newMember, registerViewData.Password);
                    Members.Login(registerViewData.Email, registerViewData.Password);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    TempData["RegistrationSuccess"] = success;
                    TempData["RegistrationErrorMessage"] = errorMessage;

                    return CurrentUmbracoPage();
                }
            }

            success = true;

            TempData["RegistrationSuccess"] = success;
            TempData["RegistrationErrorMessage"] = errorMessage;

            return CurrentUmbracoPage();
            
        }

        public bool MemberExists(string emailAddress)
        {
            return (ApplicationContext.Services.MemberService.GetByEmail(emailAddress) != null);
        }


    }
}