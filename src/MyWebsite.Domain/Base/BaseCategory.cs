using MyWebsite.Shared.Attributes;

namespace MyWebsite.Domain.Base
{
	public class BaseCategory : BaseEntity
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

	}
}
