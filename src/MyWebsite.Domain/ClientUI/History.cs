using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class History : BaseLanguageDescribe
   {
      public string TimeSpan { get; set; }
      public string Link { get; set; }
      public string Role { get; set; }
   }
}
