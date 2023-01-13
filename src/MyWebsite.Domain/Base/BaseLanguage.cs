using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Models.Base
{
   public class BaseLanguage : BaseEntity
   {
      [Required]
      public int LangId { get; set; }

      [ForeignKey(nameof(LangId))]
      public virtual Language Language { get; set; }
   }
   public class BaseLanguageDescribe : BaseLanguage
   {
      [Required]
      public string Title { get; set; }

      [Required]
      public string SubTitle { get; set; }
   }
}
