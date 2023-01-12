using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class AboutUs : BaseLanguage
   {
      [Required]
      public string FilePath { get; set; }
   }
   public class AboutUsKeyValue:KeyValue{}
}
