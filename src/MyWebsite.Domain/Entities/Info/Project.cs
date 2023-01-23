using MyWebsite.Domain.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Entities.Info
{
   public class Project : BaseLanguage
   {
      [StringLength(150)]
      [Required]
      public string Name { get; set; }
      [StringLength(200)]
      [Required]
      public string Role { get; set; }
      [Required]
      public string ImagePath { get; set; }
      [Required]
      public string Link { get; set; }
      [Required]
      public string Type { get; set; }
      [Required]
      public string DateTime { get; set; }
      [Required]
      public string Techs { get; set; }
   }
}
