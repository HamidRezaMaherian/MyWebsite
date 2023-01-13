using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Shared.Attributes
{
	public class UrlAttribute : DataTypeAttribute
	{
		public UrlAttribute()
	 : base(DataType.Url)
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.Required);
		}

		public override bool IsValid(object? value)
		{
			if (value == null)
			{
				return true;
			}

			return value is string valueAsString &&
				 (valueAsString.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
				 || valueAsString.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
				 || valueAsString.StartsWith("ftp://", StringComparison.OrdinalIgnoreCase));
		}

	}
}
