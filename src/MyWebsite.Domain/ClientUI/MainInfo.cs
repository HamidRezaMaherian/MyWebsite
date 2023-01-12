using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class MainInfo : BaseLanguage
   {
      [Required]
      public string DarkImagePath { get; set; }
      public string LightImagePath { get; set; }
      public string Description { get; set; }
   }
   public class FirstTempInfo : MainInfo
   {
   }
   public class SecondTempInfo : MainInfo { }
}
