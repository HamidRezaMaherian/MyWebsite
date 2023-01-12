using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string Title { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string SubTitle { get; set; }
   }
}
