using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using MyWebsite.Presentation.IntegrationTests.Utils;
using System.Net;

namespace MyWebsite.Presentation.Admin.IntergrationTests
{
	[TestFixture]
	public class AdminRoutesTests 
	{
		private TestingWebAppFactory<Program> _application;
		private Uri _baseUri;
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_application = new TestingWebAppFactory<Admin.Program>((serviceCollection) =>
			{
			});
			_baseUri = new Uri(_application.Server.BaseAddress.ToString());
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
		[Ignore("will be complete in next commit")]
		public async Task HomeRouteEndpoint_ReturnsOkWithProperHeader(string route, string componentName)
		{
			//var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
			//{
			//	Headless = false,
			//});
			//var page = await browser.NewPageAsync();
			//var res = await page.GotoAsync(new Uri(_baseUri, route).ToString());
			//await page.WaitForRequestAsync("**/blazor.server.js");
			//Assert.That(res!.Ok, Is.True);
			//page.Response += async (object sender, IResponse e) =>
			//{
			//	await Expect(page).ToHaveTitleAsync("MyWebsite.Presentation.Admin");
			//	Assert.That(e!.Headers.Contains(new KeyValuePair<string, string>("X-Component", componentName)), Is.True);
			//};
		}
	}
}
