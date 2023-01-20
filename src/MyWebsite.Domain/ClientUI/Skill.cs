using MyWebsite.Domain.Models.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.ClientUI
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
