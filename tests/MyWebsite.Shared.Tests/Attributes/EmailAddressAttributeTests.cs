using MyWebsite.Shared.Attributes;
using MyWebsite.Shared.Resources;
using System.Globalization;
using System.Reflection;

namespace MyWebsite.Shared.Tests.Attributes
{
    [TestFixture]
    [Culture("en-US,fa-IR")]
    public class EmailAddressAttributeTests
    {
        private string GetLocalizedMessage(Type type, string name)
        {
            var rm = new System.Resources.ResourceManager(type);
            return rm.GetString(name, CultureInfo.CurrentCulture) ?? "";
        }

        [Test]
        public void CreateObject()
        {
            var attr = new EmailAddressAttribute();
            Assert.Pass();
        }
        [Test]
        public void CheckForAttributeUsage()
        {
            var usage = typeof(EmailAddressAttribute).GetCustomAttribute<AttributeUsageAttribute>();
            Assert.IsNotNull(usage);
        }
        [Test]
        public void CheckForAttributeUsageProperties()
        {
            var usage = typeof(EmailAddressAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            Assert.Multiple(() =>
            {
                Assert.That(usage?.AllowMultiple, Is.False);
                Assert.That(usage?.ValidOn, Is.EqualTo(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property));
            });
        }
        [Test]
        public void ErrorResourceIsSet()
        {
            var attr = new EmailAddressAttribute();
            Assert.That(attr.ErrorMessageResourceType, Is.EqualTo(typeof(ErrorResource)));
            Assert.That(attr.ErrorMessageResourceName, Is.EqualTo("EmailAddress"));
        }
        [TestCase("fa-IR")]
        [TestCase("en-US")]
        public void FormatErrorMessage_ReturnValidErrorMessage(string culture)
        {
            //Arrange
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            var attr = new EmailAddressAttribute();
            //Act
            var res = attr.FormatErrorMessage("TestField");
            //Assert
            var message = string.Format(
                GetLocalizedMessage(attr.ErrorMessageResourceType!, attr.ErrorMessageResourceName!),
                "TestField");

            Assert.That(res, Is.EqualTo(message));
        }

        [TestCase("hamid", false)]
        [TestCase("hamid@.com", false)]
        [TestCase("hamid@ali", false)]
        [TestCase("hamid@ali.com", true)]
        public void GetValidationResult_PassFakeClassObject(string text, bool expectedResult)
        {
            //Arrange
            var fakeObj = new FakeClass() { Text = text };
            var attr = fakeObj.GetType().GetProperty("Text")!.GetCustomAttribute<EmailAddressAttribute>();
            //Act
            var isValid = attr!.IsValid(text);
            var getValidateResult = attr!.GetValidationResult(fakeObj.Text, new System.ComponentModel.DataAnnotations.ValidationContext(fakeObj));
            //Assert
            Assert.That(isValid, Is.EqualTo(expectedResult));
            Assert.That(getValidateResult is null, Is.EqualTo(expectedResult));
        }
        private class FakeClass
        {
            [EmailAddress]
            public string Text { get; set; }
        }
    }
}