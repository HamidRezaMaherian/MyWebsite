using MyWebsite.Shared.Attributes;
using MyWebsite.Shared.Resources;
using System.Globalization;
using System.Reflection;

namespace MyWebsite.Shared.Tests
{
	[TestFixture]
	[Culture("en-US,fa-IR")]
	public class RequiredAttributeTests
	{
		private string GetLocalizedMessage(Type type, string name)
		{
			var rm = new System.Resources.ResourceManager(type);
			return rm.GetString(name, CultureInfo.CurrentCulture) ?? "";
		}

		[Test]
		public void CreateObject()
		{
			var attr = new RequiredAttribute();
			Assert.Pass();
		}
		[Test]
		public void CheckForAttributeUsage()
		{
			var usage = typeof(RequiredAttribute).GetCustomAttribute<AttributeUsageAttribute>();
			Assert.IsNotNull(usage);
		}
		[Test]
		public void CheckForAttributeUsageProperties()
		{
			var usage = typeof(RequiredAttribute).GetCustomAttribute<AttributeUsageAttribute>();

			Assert.Multiple(() =>
			{
				Assert.That(usage?.AllowMultiple, Is.False);
				Assert.That(usage?.ValidOn, Is.EqualTo(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter));
			});
		}
		[Test]
		public void ErrorResourceIsSet()
		{
			var attr = new RequiredAttribute();
			Assert.That(attr.ErrorMessageResourceType, Is.EqualTo(typeof(ErrorResource)));
			Assert.That(attr.ErrorMessageResourceName, Is.EqualTo("Required"));
		}
		[TestCase("", true, true)]
		[TestCase("", false, false)]
		[TestCase("string", false, true)]
		[TestCase("string", true, true)]
		public void IsValid_PassObject(string value, bool allowEmptyString, bool expectedResult)
		{
			var attr = new RequiredAttribute();
			attr.AllowEmptyStrings = allowEmptyString;
			Assert.That(attr.IsValid(value), Is.EqualTo(expectedResult));
		}
		[TestCase("fa-IR")]
		[TestCase("en-US")]
		public void FormatErrorMessage_ReturnValidErrorMessage(string culture)
		{
			//Arrange
			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
			var attr = new RequiredAttribute();
			//Act
			var res = attr.FormatErrorMessage("TestField");
			//Assert
			var message = string.Format(
				GetLocalizedMessage(attr.ErrorMessageResourceType!, attr.ErrorMessageResourceName!),
				"TestField");

			Assert.That(res, Is.EqualTo(message));
		}

		[TestCase("", false)]
		[TestCase("validtext", true)]
		public void GetValidationResult_PassFakeClassObject(string text, bool isValid)
		{
			//Arrange
			var fakeObj = new FakeClass() { Text = text };
			var attr = fakeObj.GetType().GetProperty("Text")!.GetCustomAttribute<RequiredAttribute>();
			//Act
			var getValidateResult = attr!.GetValidationResult(fakeObj.Text, new System.ComponentModel.DataAnnotations.ValidationContext(fakeObj));
			//Assert
			Assert.That(getValidateResult is null, Is.EqualTo(isValid));
		}
		private class FakeClass
		{
			[Required]
			public string Text { get; set; }
		}
	}
}