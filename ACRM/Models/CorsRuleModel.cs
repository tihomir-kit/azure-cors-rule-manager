using System;
using System.Collections.Generic;
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
        public int MaxAgeInSeconds { get; set; }
    }
}