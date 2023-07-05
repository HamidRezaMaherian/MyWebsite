using MyWebsite.Presentation.Tests.Utils;
using System.Net;

namespace MyWebsite.Presentation.IntegrationTests
{
    [TestFixture]
    public class SiteRoutesTests
    {
        private TestingWebAppFactory<Program> _application;

        [SetUp]
        public void SetUp()
        {
            _application = new TestingWebAppFactory<Program>((serviceCollection) =>
            {
            });
        }
        [Test]
        public async Task SiteRoutesEndpoint_ReturnsOk()
        {
            _application = new TestingWebAppFactory<Program>();
            var client = _application.CreateClient();
            var result = await client.GetAsync("/");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
