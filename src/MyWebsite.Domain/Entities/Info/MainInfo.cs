using MyWebsite.Domain.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Entities.Info
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
