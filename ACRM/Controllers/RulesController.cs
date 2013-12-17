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
        #region Fields
        private AzureProvider _azureProvider = null;
        #endregion

        #region Constructor
        public RulesController()
        {
            ViewBag.Title = "Azure CORS rule manager"; 
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            _azureProvider = new AzureProvider(_accountName, _accountKey);
        }
        #endregion

        #region Actions
        public ActionResult Index()
        {
            var corsRules = _azureProvider.GetCorsRules();
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
            if (!ModelState.IsValid)
                return View(model);

            var corsRule = MapCorsRuleModel(model);

            try
            {
                _azureProvider.CreateCorsRule(corsRule);
            }
            catch (Exception e)
            {
                model.ExceptionMessage = e.Message;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var corsRule = _azureProvider.GetCorsRules()[id - 1];
            corsRule.AllowedMethods.ToString();
            var model = MapCorsRule(corsRule);
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, CorsRuleModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var corsRule = MapCorsRuleModel(model);

            try
            {
                _azureProvider.UpdateCorsRule(id - 1, corsRule);
            }
            catch (Exception e)
            {
                model.ExceptionMessage = e.Message;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _azureProvider.RemoveCorsRule(id - 1);
            return RedirectToAction("Index");
        }
        #endregion

        #region Mappings
        private CorsRuleModel CreateNewCorsRuleModel()
        {
            return new CorsRuleModel()
            {
                AllowedOrigins = new List<string>() { String.Empty },
                AllowedMethods = new List<string>() { String.Empty },
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
            return new CorsRuleModel()
            {
                AllowedOrigins = corsRule.AllowedOrigins,
                AllowedMethods = MapAllowedMethods(corsRule.AllowedMethods),
                AllowedHeaders = corsRule.AllowedHeaders,
                ExposedHeaders = corsRule.ExposedHeaders,
                MaxAgeInSeconds = corsRule.MaxAgeInSeconds
            };
        }

        private IList<string> MapAllowedMethods(CorsHttpMethods allowedMethods)
        {
            return allowedMethods.ToString().Replace(" ", String.Empty).Split(',').ToList();
        }

        private CorsRule MapCorsRuleModel(CorsRuleModel corsRuleModel)
        {
            return new CorsRule()
            {
                AllowedOrigins = corsRuleModel.AllowedOrigins,
                AllowedMethods = MapAllowedMethods(corsRuleModel.AllowedMethods),
                AllowedHeaders = corsRuleModel.AllowedHeaders,
                ExposedHeaders = corsRuleModel.ExposedHeaders,
                MaxAgeInSeconds = corsRuleModel.MaxAgeInSeconds
            };
        }

        private CorsHttpMethods MapAllowedMethods(IList<string> allowedMethods)
        {
            var corsHttpMethods = new CorsHttpMethods();

            foreach (var allowedMethod in allowedMethods)
            {
                var tmpMethod = new CorsHttpMethods();
                if (Enum.TryParse<CorsHttpMethods>(allowedMethod, true, out tmpMethod))
                    corsHttpMethods |= tmpMethod;
            }

            return corsHttpMethods;
        }
        #endregion
    }
}
