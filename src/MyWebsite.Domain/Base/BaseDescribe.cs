using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Models.Base
{
   public class BaseDescribe : BaseEntity
   {
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string Title { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string SubTitle { get; set; }
   }
}
