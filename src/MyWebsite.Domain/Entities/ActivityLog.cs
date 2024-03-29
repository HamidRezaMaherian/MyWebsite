﻿using MyWebsite.Domain.Base;
using MyWebsite.Domain.Entities.User;
using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Entities
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

      [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(UserId))]
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
