using MyWebsite.Presentation.IntegrationTests.Utils;
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
			_application = new TestingWebAppFactory<Program>();
		}
		[TestCase("/", "Home", "Index")]
		[TestCase("/home", "Home", "Index")]
		[TestCase("/home/index", "Home", "Index")]
		public async Task SiteRoutesEndpoint_ReturnsOk(string route, string controller, string action)
		{
			var client = _application.CreateClient();
			var result = await client.GetAsync(route);
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result.Headers.First(i => i.Key == "X-Controller").Value.Single(), Is.EqualTo(controller));
			Assert.That(result.Headers.First(i => i.Key == "X-Action").Value.Single(), Is.EqualTo(action));
		}
	}
}
