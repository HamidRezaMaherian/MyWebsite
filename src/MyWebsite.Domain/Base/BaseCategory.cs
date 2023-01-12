using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyWebsite.Domain.Models.Base
{
   public class BaseCategory : BaseEntity
   {
      [Required]
      [MaxLength(50)]
      public string Name { get; set; }

   }
}
