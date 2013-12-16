using ACRM.Infrastructure;
using ACRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACRM.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = new AzureCredentialsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AzureCredentialsModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!AzureProvider.GetInstance(model.AccountName, model.AccountKey).AzureClientExists())
            {
                model.CustomErrorMessage = "Invalid credentials.";
                return View(model);
            }

            SetCredentialsCookie(model.AccountName, model.AccountKey);
            return RedirectToAction("Index", "Rules");
        }
    }
}
