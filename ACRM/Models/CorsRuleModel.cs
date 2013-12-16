using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRM.Models
{
    public class CorsRuleModel
    {
        public int Id { get; set; }
        public IList<string> AllowedOrigins { get; set; }
        public IList<string> AllowedMethods { get; set; }
        public IList<string> AllowedHeaders { get; set; }
        public IList<string> ExposedHeaders { get; set; }

        [Display(Name = "Max Age in Seconds")]
        public int MaxAgeInSeconds { get; set; }

        public string ExceptionMessage { get; set; }
    }
}