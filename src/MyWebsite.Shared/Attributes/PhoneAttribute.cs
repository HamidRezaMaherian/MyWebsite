﻿using MyWebsite.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Shared.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
	public class PhoneAttribute : DataTypeAttribute
	{
		private const string AdditionalPhoneNumberCharacters = "-.()";
		private const string ExtensionAbbreviationExtDot = "ext.";
		private const string ExtensionAbbreviationExt = "ext";
		private const string ExtensionAbbreviationX = "x";

		public PhoneAttribute()
			 : base(DataType.PhoneNumber)
		{
			ErrorMessageResourceType = typeof(ErrorResource);
			ErrorMessageResourceName = nameof(ErrorResource.Phone);
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

			valueAsString = valueAsString.Replace("+", string.Empty).TrimEnd();
			valueAsString = RemoveExtension(valueAsString);

			bool digitFound = false;
			foreach (char c in valueAsString)
			{
				if (char.IsDigit(c))
				{
					digitFound = true;
					break;
				}
			}

			if (!digitFound)
			{
				return false;
			}

			foreach (char c in valueAsString)
			{
				if (!(char.IsDigit(c)
					 || char.IsWhiteSpace(c)
					 || AdditionalPhoneNumberCharacters.IndexOf(c) != -1))
				{
					return false;
				}
			}

			return true;
		}

		private static string RemoveExtension(string potentialPhoneNumber)
		{
			var lastIndexOfExtension = potentialPhoneNumber
				 .LastIndexOf(ExtensionAbbreviationExtDot, StringComparison.OrdinalIgnoreCase);
			if (lastIndexOfExtension >= 0)
			{
				var extension = potentialPhoneNumber.Substring(
					 lastIndexOfExtension + ExtensionAbbreviationExtDot.Length);
				if (MatchesExtension(extension))
				{
					return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
				}
			}

			lastIndexOfExtension = potentialPhoneNumber
				 .LastIndexOf(ExtensionAbbreviationExt, StringComparison.OrdinalIgnoreCase);
			if (lastIndexOfExtension >= 0)
			{
				var extension = potentialPhoneNumber.Substring(
					 lastIndexOfExtension + ExtensionAbbreviationExt.Length);
				if (MatchesExtension(extension))
				{
					return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
				}
			}

			lastIndexOfExtension = potentialPhoneNumber
				 .LastIndexOf(ExtensionAbbreviationX, StringComparison.OrdinalIgnoreCase);
			if (lastIndexOfExtension >= 0)
			{
				var extension = potentialPhoneNumber.Substring(
					 lastIndexOfExtension + ExtensionAbbreviationX.Length);
				if (MatchesExtension(extension))
				{
					return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
				}
			}

			return potentialPhoneNumber;
		}

		private static bool MatchesExtension(string potentialExtension)
		{
			potentialExtension = potentialExtension.TrimStart();
			if (potentialExtension.Length == 0)
			{
				return false;
			}

			foreach (char c in potentialExtension)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}

			return true;
		}
	}

}
