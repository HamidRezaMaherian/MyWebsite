using MyWebsite.Domain.Models.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class Company : BaseLanguage
   {
      [Required]
      [StringLength(100)]
      public string Name { get; set; }
      [Required]
      public string ImagePath { get; set; }
   }
}
