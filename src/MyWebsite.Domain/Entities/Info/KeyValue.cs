using MyWebsite.Domain.Base;

namespace MyWebsite.Domain.Entities.Info
{
   public class KeyValue : BaseLanguage
   {
      public string Key { get; set; }
      public string Value { get; set; }
   }
}
