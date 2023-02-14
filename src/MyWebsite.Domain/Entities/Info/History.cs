using MyWebsite.Domain.Base;

namespace MyWebsite.Domain.Entities.Info
{
   public abstract class History : BaseLanguageDescribe
   {
      public string TimeSpan { get; set; }
      public string Link { get; set; }
      public string Role { get; set; }
   }
}
