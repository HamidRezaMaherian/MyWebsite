using MyWebsite.Shared.Attributes;
using MyWebsite.Shared.Resources;
using System.Globalization;
using System.Reflection;

namespace MyWebsite.Shared.Tests.Attributes
{
    [TestFixture]
    [Culture("en-US,fa-IR")]
    public class UrlAttributeTests
    {
        private string GetLocalizedMessage(Type type, string name)
        {
            var rm = new System.Resources.ResourceManager(type);
            return rm.GetString(name, CultureInfo.CurrentCulture) ?? "";
        }

        [Test]
        public void CreateObject()
        {
            var attr = new UrlAttribute();
            Assert.Pass();
        }
        [Test]
        public void CheckForAttributeUsage()
        {
            var usage = typeof(UrlAttribute).GetCustomAttribute<AttributeUsageAttribute>();
            Assert.IsNotNull(usage);
        }
        [Test]
        public void CheckForAttributeUsageProperties()
        {
            var usage = typeof(UrlAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            Assert.Multiple(() =>
            {
                Assert.That(usage?.AllowMultiple, Is.False);
                Assert.That(usage?.ValidOn, Is.EqualTo(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property));
            });
        }
        [Test]
        public void ErrorResourceIsSet()
        {
            var attr = new UrlAttribute();
            Assert.That(attr.ErrorMessageResourceType, Is.EqualTo(typeof(ErrorResource)));
            Assert.That(attr.ErrorMessageResourceName, Is.EqualTo("Url"));
        }
        [TestCase("fa-IR")]
        [TestCase("en-US")]
        public void FormatErrorMessage_ReturnValidErrorMessage(string culture)
        {
            //Arrange
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            var attr = new UrlAttribute();
            //Act
            var res = attr.FormatErrorMessage("TestField");
            //Assert
            var message = string.Format(
                GetLocalizedMessage(attr.ErrorMessageResourceType!, attr.ErrorMessageResourceName!),
                "TestField");

            Assert.That(res, Is.EqualTo(message));
        }

        [TestCase("htt", false)]
        [TestCase("https://hamidrm", true)]
        [TestCase("https://hamidrm.ir", true)]
        public void GetValidationResult_PassFakeClassObject(string text, bool expectedResult)
        {
            //Arrange
            var fakeObj = new FakeClass() { Text = text };
            var attr = fakeObj.GetType().GetProperty("Text")!.GetCustomAttribute<UrlAttribute>();
            //Act
            var isValid = attr!.IsValid(text);
            var getValidateResult = attr!.GetValidationResult(fakeObj.Text, new System.ComponentModel.DataAnnotations.ValidationContext(fakeObj));
            //Assert
            Assert.That(isValid, Is.EqualTo(expectedResult));
            Assert.That(getValidateResult is null, Is.EqualTo(expectedResult));
        }
        private class FakeClass
        {
            [Url]
            public string Text { get; set; }
        }
    }
}