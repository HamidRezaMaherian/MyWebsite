using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MyWebsite.Shared.Attributes
{
	public class StringLengthAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
	{
		public StringLengthAttribute(int maximumLength) : base(maximumLength)
		{
			if (maximumLength is < 1)
			{
				throw new InvalidOperationException("Maximum Length should be greater than 1");
			}
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.StringLength);
		}
		public new int MinimumLength
		{
			get => base.MinimumLength;
			set
			{
				if ((value is >= 0) && (value <= MaximumLength))
					base.MinimumLength = value;
				else
					throw new InvalidOperationException("Value should be a positive number less than or equal to maximumlength");
			}

		}
		public override string FormatErrorMessage(string name)
		{
			return base.FormatErrorMessage(name);
		}
	}
}
