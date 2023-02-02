using Microsoft.AspNetCore.Identity;
using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Entities.User
{
   public class ApplicationUser : IdentityUser
   {
      public DateTime SignUpDateTime { get; set; }
      public DateTime LastLoginDateTime { get; set; }
      public string ImagePath { get; set; }
      public string FullName { get; set; }
		[Display(ResourceType = typeof(EntityResource))]
		public bool IsActive { get; set; }
		[Display(ResourceType = typeof(EntityResource))]
		public bool IsDelete { get; set; }
   }
}
