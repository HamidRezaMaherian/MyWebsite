using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Shared.Attributes
{
	public class MyRequiredAttribute : RequiredAttribute
	{
		public MyRequiredAttribute()
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.Required);
			ErrorMessage = nameof(ErrorResource.Required);
		}
	}
}
