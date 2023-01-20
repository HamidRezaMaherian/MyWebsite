using MyWebsite.Domain.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Entities.Info
{
   public class Skill : BaseEntity
   {
      [Required]
      [StringLength(100)]
      public string Name { get; set; }
      [Required]
      public int Value { get; set; }
   }
}
