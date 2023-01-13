using MyWebsite.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using MyWebsite.Domain.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Models
{
   public class ActivityLog : BaseEntity
   {
      [Required]
      public ActivityType ActivityType { get; set; }
      [Required]
      public string UserId { get; set; }
      [Required]
      public DateTime DateTime { get; set; }
      [Required]
      public string EntityId { get; set; }
      [Required]
      public string EntityType { get; set; }

      #region NavigationProps

      [ForeignKey(nameof(UserId))]
      public virtual ApplicationUser User { get; set; }

      #endregion
   }
   public enum ActivityType
   {
      Add = 1,
      Update = 2,
      Delete = 3
   }
}
