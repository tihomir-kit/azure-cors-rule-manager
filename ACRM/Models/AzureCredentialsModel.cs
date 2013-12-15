using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRM.Models
{
    public class AzureCredentialsModel
    {
        [Required]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required]
        [Display(Name = "Account Key")]
        public string AccountKey { get; set; }

        public string CustomErrorMessage { get; set; }
    }
}