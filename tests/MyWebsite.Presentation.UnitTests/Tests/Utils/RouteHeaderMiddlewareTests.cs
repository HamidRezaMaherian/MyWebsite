using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Diagnostics.Contracts;

namespace MyWebsite.Presentation.UnitTests.Utils
{
	[TestFixture]
	public class RouteHeaderMiddlewareTests
	{
		[Test]
		public void CreateMiddleware()
		{
			var del = (HttpContext ctx) => Task.CompletedTask;
			Assert.Throws<ArgumentNullException>(() => new RouteHeaderMiddleware(null));
			var mdw = new RouteHeaderMiddleware(new RequestDelegate(del));
		}
		[TestCase("Home", "Index")]
		[TestCase("HomeController", "Index")]
		public void InvokeMiddleware_AddRouteHeaders(string controller, string action)
		{
			var controllerActionMetadata = new ControllerActionDescriptor()
			{
				ControllerName = controller,
				ActionName = action
			};
			var context = CreateContext("/");
			var endpointMetadataCollection = new EndpointMetadataCollection(controllerActionMetadata);
			context.SetEndpoint(new Endpoint((HttpContext ctx) => Task.CompletedTask, endpointMetadataCollection, "Test"));
			var del = (HttpContext ctx) =>
			{
				Assert.That(ctx.Response.Headers["X-Controller"].ToString(), Is.EqualTo(controllerActionMetadata.ControllerName.Replace("Controller", "")));
				Assert.That(ctx.Response.Headers["X-Action"].ToString(), Is.EqualTo(controllerActionMetadata.ActionName));
				return Task.CompletedTask;
			};
			var mdw = new RouteHeaderMiddleware(new RequestDelegate(del));
			mdw.Invoke(context);
		}

		private HttpContext CreateContext(string path)
		{
			var req = new FakeHttpRequest()
			{
				Path = path,
				Method = HttpMethod.Get.ToString(),
				PathBase = path,
			};
			var res = new FakeHttpResponse(
				new HeaderDictionary(
					new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()
					)
				);
			return new FakeHttpContext(req, res);
		}
	}
}
