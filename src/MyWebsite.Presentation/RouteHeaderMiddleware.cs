using Microsoft.AspNetCore.Mvc.Controllers;

namespace MyWebsite.Presentation
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class RouteHeaderMiddleware
	{
		private readonly RequestDelegate _next;

		public RouteHeaderMiddleware(RequestDelegate next)
		{
			ArgumentNullException.ThrowIfNull(next);
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{
			var endpointInfo = httpContext.GetEndpoint()!.Metadata.OfType<ControllerActionDescriptor>().FirstOrDefault();
			httpContext.Response.Headers.Append("X-Controller", endpointInfo!.ControllerName.Replace("Controller", ""));
			httpContext.Response.Headers.Append("X-Action", endpointInfo!.ActionName);
			return _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class RouteHeaderMiddlewareExtensions
	{
		public static IApplicationBuilder UseRouteHeaderMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RouteHeaderMiddleware>();
		}
	}
}
