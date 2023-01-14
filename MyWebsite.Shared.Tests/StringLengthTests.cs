using MyWebsite.Shared.Attributes;
using MyWebsite.Shared.Resources;
using System.Globalization;
using System.Reflection;

namespace MyWebsite.Shared.Tests
{
	[TestFixture]
	[Culture("en-US,fa-IR")]
	public class StringLengthTests
	{
		private string GetLocalizedMessage(Type type, string name)
		{
			var rm = new System.Resources.ResourceManager(type);
			return rm.GetString(name, CultureInfo.CurrentCulture) ?? "";
		}

		[Test]
		public void CreateObject_WithValidLength()
		{
			var attr = new StringLengthAttribute(10);
			Assert.That(attr.MaximumLength, Is.EqualTo(10));
		}
		[Test]
		public void Create_CreateObject_WithInvalidLength()
		{
			Assert.Throws<InvalidOperationException>(() =>
			{
				var attr = new StringLengthAttribute(-10);
			});
		}
		[TestCase(12)]
		[TestCase(-2)]
		public void SetMiminumLength_WithInvalidValue(int min)
		{
			var attr = new StringLengthAttribute(10);
			Assert.Throws<InvalidOperationException>(() =>
			{
				attr.MinimumLength = min;
			});
		}
		[Test]
		public void SetMiminumLength_WithValidValue()
		{
			var attr = new StringLengthAttribute(10);
			attr.MinimumLength = 5;
			Assert.That(attr.MinimumLength, Is.EqualTo(5));
		}
		[Test]
		public void ErrorResourceIsSet()
		{
			var attr = new StringLengthAttribute(10);
			Assert.That(attr.ErrorMessageResourceType, Is.EqualTo(typeof(ErrorResource)));
			Assert.That(attr.ErrorMessageResourceName, Is.EqualTo("StringLength"));
		}
		[Test]
		[TestCase("fa-IR")]
		[TestCase("en-US")]
		public void FormatErrorMessage_ReturnValidErrorMessage(string culture)
		{
			//Arrange
			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
			var attr = new StringLengthAttribute(10);
			attr.MinimumLength = 2;
			//Act
			var res = attr.FormatErrorMessage("TestField");
			//Assert
			var message = string.Format(
				GetLocalizedMessage(attr.ErrorMessageResourceType!, attr.ErrorMessageResourceName!),
				"TestField",
				attr.MaximumLength,
				attr.MinimumLength);

			Assert.That(res, Is.EqualTo(message));
		}

		[TestCase("invalid text string", false)]
		[TestCase("validtext", true)]
		public void ValidationMethods_PassFakeClassObject(string text, bool isValid)
		{
			//Arrange
			var fakeObj = new FakeClass() { Text = text };
			var attr = fakeObj.GetType().GetProperty("Text")!.GetCustomAttribute<StringLengthAttribute>();
			//Act
			var isValidResult = attr!.IsValid(fakeObj.Text);
			var getValidateResult = attr!.GetValidationResult(fakeObj.Text, new System.ComponentModel.DataAnnotations.ValidationContext(fakeObj));
			//Assert
			Assert.That(isValidResult, Is.EqualTo(isValid));
			Assert.That(getValidateResult is null, Is.EqualTo(isValid));
		}

		private class FakeClass
		{
			[StringLength(10, MinimumLength = 2)]
			public string Text { get; set; }
		}
	}
}