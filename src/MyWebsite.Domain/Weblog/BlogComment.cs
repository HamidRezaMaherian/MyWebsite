using MyWebsite.Domain.Models.Base;
using MyWebsite.Domain.Models.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Models.Weblog
{
   public class BlogComment : BaseEntity
   {

      [Required]
      [Display(Name = "نام")]
      public string Author { get; set; }
      [Required]
      [Display(Name = "ایمیل")]
      [MaxLength(100)]
      public string Email { get; set; }

      [Required]
      public int BlogId { get; set; }

      public string UserId { get; set; }

      [Required]
      public DateTime CommentDateTime { get; set; }
      [Required]
      [Display(Name = "نظر")]
      [MaxLength(500)]
      public string Comment { get; set; }

      public bool HasAnswer { get; set; }
      [Display(Name = "پاسخ")]
      [MaxLength(500)]
      public string Answer { get; set; }
      public DateTime AnswerDateTime { get; set; }

      #region NavigationProps
      [ForeignKey(nameof(UserId))]
      public virtual ApplicationUser User { get; set; }
      [ForeignKey(nameof(BlogId))]
      public virtual Blog Blog { get; set; }

      #endregion
   }
}
