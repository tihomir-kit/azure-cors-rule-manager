using ACRM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACRM.Controllers
{
    public class RulesController : ControllerBase
    {
        public ActionResult Index()
        {
            var corsRules = AzureProvider.GetInstance().GetCorsRules(_accountName, _accountKey);

            return View();
        }
    }
}
