using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyWebsite.Shared.Attributes
{
	public class StringLengthAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
	{
		public StringLengthAttribute(int maximumLength) : base(maximumLength)
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.StringLength);
		}
	}
}
