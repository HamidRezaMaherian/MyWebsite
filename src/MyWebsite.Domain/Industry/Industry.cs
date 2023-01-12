using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyWebsite.Domain.Models.Base;
using MyWebsite.Domain.Models.ClientUI;
using MyWebsite.Domain.Models.User;

namespace MyWebsite.Domain.Models.Services
{
    public class Industry : BaseLanguageDescribe
    {
        public string Link { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
