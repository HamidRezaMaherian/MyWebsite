namespace MyWebsite.Presentation.Admin
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class RouteHeaderMiddleware
	{
		private readonly RequestDelegate _next;

		public RouteHeaderMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task Invoke(HttpContext httpContext)
		{

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
