using MyWebsite.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyWebsite.Domain.Models
{
    public class Language:BaseEntity
    {
      [Required]
      [MaxLength(50)]
      public string Culture { get; set; }
   }
}
