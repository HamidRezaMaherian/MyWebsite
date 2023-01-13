using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Models.User
{
   public class ApplicationUser
   {
      public DateTime SignUpDateTime { get; set; }
      public DateTime LastLoginDateTime { get; set; }
      public string ImagePath { get; set; }
      public string FullName { get; set; }
      [Display(Name = "وضعیت نمایش")]
      public bool IsActive { get; set; }
      public bool IsDelete { get; set; }
   }
}
