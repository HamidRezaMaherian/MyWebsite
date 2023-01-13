using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.Base;

namespace MyWebsite.Domain.Models.ClientUI
{
   public class ContactMe : BaseLanguage
   {
      [Required]
      [EmailAddress]
      [MaxLength(50)]
      public string Email { get; set; }
      [Phone]
      [Required]
      [MaxLength(11)]
      public string PhoneNumber { get; set; }
      [Url]
      [MaxLength(150)]
      public string Instagram { get; set; }
      [Url]
      [MaxLength(150)]
      public string Linkedin { get; set; }
      [MaxLength(150)]
      [Url]
      public string Telegram { get; set; }
      [MaxLength(150)]
      public string WhatsApp { get; set; }
      [MaxLength(150)]
      public string Twitter { get; set; }
      [MaxLength(150)]
      public string FaceBook { get; set; }
      //[Url]
      //[MaxLength(150)]
      //public string Aparat { get; set; }
      //[Url]
      //[MaxLength(150)]
      //public string Youtube { get; set; }
   }
}
