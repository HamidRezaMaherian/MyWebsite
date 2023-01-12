
using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class KeyValue: BaseLanguage
   {
      public string Key { get; set; }
      public string Value { get; set; }
   }
}
