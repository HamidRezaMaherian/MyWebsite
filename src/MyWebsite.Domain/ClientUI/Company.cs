using MyWebsite.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class Company : BaseLanguage
   {
      [Display(Name = "نام")]
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [MaxLength(100, ErrorMessage = "توضیحات کوتاه نباید بیش از {1} کاراکتر باشد")]
      public string Name { get; set; }
      [Required]
      public string ImagePath { get; set; }
   }
}
