using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACRM.Controllers
{
    public class ControllerBase : Controller
    {
        protected string _accountName = null;
        protected string _accountKey = null;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ReadCredentialsCookie();
        }

        protected void SetCredentialsCookie(string accountName, string accountKey)
        {
            HttpCookie cookie = new HttpCookie("azureCookie");
            cookie.Values.Add("accountName", accountName);
            cookie.Values.Add("accountKey", accountKey);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
        }

        protected ActionResult ReadCredentialsCookie()
        {
            HttpCookie cookie = Request.Cookies["azureCookie"];
            if (cookie == null)
                return RedirectToAction("Index", "Home");

            var accountName = cookie.Values["accountName"];
            var accountKey = cookie.Values["accountKey"];

            if (String.IsNullOrEmpty(accountName) || String.IsNullOrEmpty(accountKey))
                return RedirectToAction("Index", "Home");

            _accountName = accountName;
            _accountKey = accountKey;

            return null;
        }
    }
}
