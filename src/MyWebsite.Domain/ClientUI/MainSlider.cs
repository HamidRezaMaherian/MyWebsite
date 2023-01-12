using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class MainSlider : BaseLanguage
   {
      [Required]
      public string ImagePath { get; set; }
   }
}
