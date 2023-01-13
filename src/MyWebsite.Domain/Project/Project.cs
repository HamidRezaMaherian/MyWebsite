using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.Projects
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
