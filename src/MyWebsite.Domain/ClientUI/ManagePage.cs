using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
    public class ManagePage : BaseLanguageDescribe
    {
        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
        
        [Required]
[MaxLength(50)]
        public string AdminName { get; set; }
    }
}
