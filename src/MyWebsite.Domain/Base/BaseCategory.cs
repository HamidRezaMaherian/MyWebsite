using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.Base
{
	public class BaseCategory : BaseEntity
	{
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

	}
}
