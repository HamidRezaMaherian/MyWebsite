using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Models.User
{
   public class ApplicationUser
   {
      public DateTime SignUpDateTime { get; set; }
      public DateTime LastLoginDateTime { get; set; }
      public string ImagePath { get; set; }
      public string FullName { get; set; }
		[Display(ResourceType = typeof(DomainResource))]
		public bool IsActive { get; set; }
		[Display(ResourceType = typeof(DomainResource))]
		public bool IsDelete { get; set; }
   }
}
