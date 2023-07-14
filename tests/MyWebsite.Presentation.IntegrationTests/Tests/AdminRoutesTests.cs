using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using MyWebsite.Presentation.IntegrationTests.Utils;
using System.Net;

namespace MyWebsite.Presentation.Admin.IntergrationTests
{
	[TestFixture]
	public class AdminRoutesTests : PageTest
	{
		private TestingWebAppFactory<Program> _application;
		private Uri _baseUri;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_application = new TestingWebAppFactory<Admin.Program>((serviceCollection) =>
			{
			});
			_baseUri = new Uri(_application.ServerAddress);
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_application.Dispose();
		}
		[Test]
		public async Task HealthCheckEndpoint_ReturnsOk()
		{
			var client = _application.CreateClient();
			var result = await client.GetAsync("/healthstatus");
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[TestCase("/", "Home")]
		[TestCase("/home", "Home")]
		public async Task HomeRouteEndpoint_ReturnsOkWithProperHeader(string route, string title)
		{
			var res = await Page.GotoAsync(new Uri(_baseUri, route).ToString(), new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });
			await Expect(Page).ToHaveTitleAsync(title);
			Assert.That(res!.Ok, Is.True);
		}
	}
}
