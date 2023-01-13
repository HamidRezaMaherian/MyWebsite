using MyWebsite.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Models
{
   public class Language:BaseEntity
    {
      [Required]
      [MaxLength(50)]
      public string Culture { get; set; }
   }
}
