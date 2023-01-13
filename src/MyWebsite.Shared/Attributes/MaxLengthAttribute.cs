using MyWebsite.Shared.Resources;

namespace MyWebsite.Shared.Attributes
{
	public class MaxLengthAttribute : System.ComponentModel.DataAnnotations.MaxLengthAttribute
	{
		public MaxLengthAttribute() : base()
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.Required);
			ErrorMessage = nameof(ErrorResource.Required);
		}

		public MaxLengthAttribute(int length) : base(length)
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.MaxLength);
			ErrorMessage = nameof(ErrorResource.MaxLength);
		}
	}
}
