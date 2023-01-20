using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Models.Base
{
	public class BaseCategory : BaseEntity
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

	}
}
