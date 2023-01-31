using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Domain.Base
{
	public class BaseEntity
	{
		[Key]
		public int Id { get; set; }
		[Display(ResourceType = typeof(EntityResource))]
		public bool IsActive { get; set; }
		[Display(ResourceType = typeof(EntityResource))]
		public bool IsDelete { get; set; }
	}
}
