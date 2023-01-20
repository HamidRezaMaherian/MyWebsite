using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Shared.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public class EmailAddressAttribute : DataTypeAttribute
	{
		private static bool EnableFullDomainLiterals { get; } =
			 AppContext.TryGetSwitch("System.Net.AllowFullDomainLiterals", out bool enable) ? enable : false;

		public EmailAddressAttribute()
			 : base(DataType.EmailAddress)
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.EmailAddress);
		}

		public override bool IsValid(object? value)
		{
			if (value == null)
			{
				return true;
			}

			if (!(value is string valueAsString))
			{
				return false;
			}

			if (!EnableFullDomainLiterals && (valueAsString.Contains('\r') || valueAsString.Contains('\n')))
			{
				return false;
			}

			// only return true if there is only 1 '@' character
			// and it is neither the first nor the last character
			return
				IsAtSignCorrect(valueAsString) &&
				IsNextAndPreviousCorrect(valueAsString);
		}

		private static bool IsNextAndPreviousCorrect(string valueAsString)
		{
			int index = valueAsString.IndexOf('@');
			var nextChar = valueAsString[index + 1];
			var prevChar = valueAsString[index + 1];
			
			return		 nextChar is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') &&
							 prevChar is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') &&
							 valueAsString.IndexOf('.') > index;
		}

		private static bool IsAtSignCorrect(string valueAsString)
		{
			int index = valueAsString.IndexOf('@');
			return index > 0 &&
							 index != valueAsString.Length - 1 &&
							 index == valueAsString.LastIndexOf('@');
		}
	}
}
