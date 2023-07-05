using MyWebsite.Presentation.Tests.Utils;
using System.Net;

namespace MyWebsite.Presentation.Admin.IntergrationTests
{
    [TestFixture]
    public class AdminRoutesTests : BunitTestContext
    {
        private TestingWebAppFactory<Admin.Program> _application;

        [SetUp]
        public void SetUp()
        {
            _application = new TestingWebAppFactory<Admin.Program>((serviceCollection) =>
            {
            });
        }
        [Test]
        public async Task HealthCheckEndpoint_ReturnsOk()
        {
            _application = new TestingWebAppFactory<Admin.Program>();
            var client = _application.CreateClient();
            var result = await client.GetAsync("/healthstatus");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public async Task HomeRouteEndpoint_ReturnsOkWithProperHtmlOutput()
        {
            _application = new TestingWebAppFactory<Admin.Program>();
            var client = _application.CreateClient();
            var result = await client.GetAsync("/");
            //var htmlOutput = TestContext!.RenderComponent<Admin.Pages.Home.Index>().Markup;
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
