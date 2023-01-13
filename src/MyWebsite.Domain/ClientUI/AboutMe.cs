using MyWebsite.Domain.Models.Base;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class AboutMe : BaseLanguage
   {
      [Required]
      public string FilePath { get; set; }
   }
   public class AboutUsKeyValue : KeyValue { }
}
