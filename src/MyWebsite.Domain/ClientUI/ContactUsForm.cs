using System;
using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;
using MyWebsite.Domain.Models.User;

namespace MyWebsite.Domain.Models.ClientUI
{
    public class ContactUsForm : BaseEntity
    {
        public string UserId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "RequiredAttribute_ValidationError")]
        public string Name { get; set; }
      [Display(Name = "ایمیل")]
      [EmailAddress(ErrorMessage = "لطفا ایمیل خود را صحیح وارد کنید")]
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string Email { get; set; }
      [Display(Name = "ایمیل")]
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      public string Subject { get; set; }

      [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Message { get; set; }

        [Display(Name = "پاسخ")]
        [MaxLength(500)]
        public string Answer { get; set; }

        public bool IsAnswered { get; set; }
        public DateTime QuestionDateTime { get; set; }
        public DateTime AnswerDateTime { get; set; }

        #region NavigationProps
        public virtual ApplicationUser User { get; set; }
        #endregion
    }
}
