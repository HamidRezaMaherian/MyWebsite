using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class AboutMe : BaseLanguage
   {
      [Required]
      public string FilePath { get; set; }
   }
   public class AboutUsKeyValue:KeyValue{}
}
