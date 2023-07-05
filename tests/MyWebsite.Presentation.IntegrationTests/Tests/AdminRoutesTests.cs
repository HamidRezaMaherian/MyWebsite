using Microsoft.AspNetCore.Mvc;
using MyWebsite.Presentation.IntegrationTests.Utils;
using System;
using System.Net;

namespace MyWebsite.Presentation.Admin.IntergrationTests
{
	[TestFixture]
	public class AdminRoutesTests : BunitTestContext
	{
		private TestingWebAppFactory<Program> _application;

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
		[TestCase("/", "Home")]
		[TestCase("/home", "Home")]
		[Ignore("will be complete in next commit")]
		public async Task HomeRouteEndpoint_ReturnsOkWithProperHeader(string route, string componentName)
		{
			_application = new TestingWebAppFactory<Admin.Program>();
			var client = _application.CreateClient();
			var result = await client.GetAsync(route);
			Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result.Headers.First(i => i.Key == "X-Component").Value.Single(), Is.EqualTo(componentName));
		}
	}
}
