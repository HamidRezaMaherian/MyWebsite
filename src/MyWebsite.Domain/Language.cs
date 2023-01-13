using MyWebsite.Domain.Models.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models
{
   public class Language:BaseEntity
    {
      [Required]
      [MaxLength(50)]
      public string Culture { get; set; }
   }
}
