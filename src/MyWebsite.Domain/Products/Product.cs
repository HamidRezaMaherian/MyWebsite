using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.Products
{
   public class Product : BaseLanguage
   {
      [Required]
      public string Name { get; set; }
      [Required]
      public string ImagePath { get; set; }

      [Required]
      public string Content { get; set; }

      public int Visit { get; set; }
   }
}
