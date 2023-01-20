using MyWebsite.Shared.Resources;

namespace MyWebsite.Shared.Attributes
{
	public class RequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
	{
		public RequiredAttribute()
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.Required);
		}
	}
}
