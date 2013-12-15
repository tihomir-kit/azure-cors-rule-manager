using ACRM.Infrastructure;
using ACRM.Models;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACRM.Controllers
{
    public class RulesController : ControllerBase
    {
        #region Constructor
        public RulesController()
        {
            ViewBag.Title = "Azure CORS rule manager"; 
        }
        #endregion

        #region Actions
        public ActionResult Index()
        {
            var corsRules = AzureProvider.GetInstance().GetCorsRules(_accountName, _accountKey);
            var model = MapCorsRules(corsRules);

            return View(model);       
        }

        public ActionResult Create()
        {
            var model = CreateNewCorsRuleModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CorsRuleModel model)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var corsRule = AzureProvider.GetInstance().GetCorsRules(_accountName, _accountKey)[id - 1];
            var model = MapCorsRule(corsRule);
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CorsRuleModel model)
        {
            return View(model);
        }
        #endregion

        #region Mappings
        private CorsRuleModel CreateNewCorsRuleModel()
        {
            return new CorsRuleModel()
            {
                AllowedOrigins = new List<string>() { String.Empty },
                AllowedHeaders = new List<string>() { String.Empty },
                ExposedHeaders = new List<string>() { String.Empty },
                MaxAgeInSeconds = 0
            };
        }

        private CorsRulesModel MapCorsRules(IList<CorsRule> corsRules)
        {
            IList<CorsRuleModel> mappedCorsRules = new List<CorsRuleModel>();

            int id = 1;
            foreach (var corsRule in corsRules)
            {
                var mappedCorsRule = MapCorsRule(corsRule);
                mappedCorsRule.Id = id++;
                mappedCorsRules.Add(mappedCorsRule);
            }

            return new CorsRulesModel() { CorsRules = mappedCorsRules };
        }

        private CorsRuleModel MapCorsRule(CorsRule corsRule)
        {
            // TODO: implement allowed headers mapping

            return new CorsRuleModel()
            {
                AllowedOrigins = corsRule.AllowedOrigins,
                AllowedHeaders = corsRule.AllowedHeaders,
                ExposedHeaders = corsRule.ExposedHeaders,
                MaxAgeInSeconds = corsRule.MaxAgeInSeconds
            };
        }
        #endregion
    }
}
