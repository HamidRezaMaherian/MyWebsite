using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Base
{
   public class BaseDescribe : BaseEntity
   {
      [Required]
      public string Title { get; set; }

      [Required]
      public string SubTitle { get; set; }
   }
}
