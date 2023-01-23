using MyWebsite.Domain.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Entities
{
   public class Language : BaseEntity
   {
      [Required]
      [StringLength(50)]
      public string Culture { get; set; }
   }
}
