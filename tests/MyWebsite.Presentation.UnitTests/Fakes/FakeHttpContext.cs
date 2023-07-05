using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;

namespace MyWebsite.Presentation.UnitTests.Utils
{
	public class FakeHttpContext : HttpContext
	{
		private readonly HttpRequest request;
		private readonly HttpResponse response;

		public FakeHttpContext(HttpRequest request, HttpResponse response)
		{
			this.request = request;
			this.response = response;
			this.Features = new FeatureCollection();
		}
		public override IFeatureCollection Features { get; }

		public override HttpRequest Request => request;

		public override HttpResponse Response => response;

		public override ConnectionInfo Connection { get; }

		public override WebSocketManager WebSockets { get; }

		public override ClaimsPrincipal User { get; set; }
		public override IDictionary<object, object> Items { get; set; }
		public override IServiceProvider RequestServices { get; set; }
		public override CancellationToken RequestAborted { get; set; }
		public override string TraceIdentifier { get; set; }
		public override ISession Session { get; set; }

		public override void Abort()
		{
			;
		}
	}

	public class FakeHttpRequest : HttpRequest
	{

		public override HttpContext HttpContext { get; }

		public override string Method { get; set; }
		public override string Scheme { get; set; }
		public override bool IsHttps { get; set; }
		public override HostString Host { get; set; }
		public override PathString PathBase { get; set; }
		public override PathString Path { get; set; }
		public override QueryString QueryString { get; set; }
		public override IQueryCollection Query { get; set; }
		public override string Protocol { get; set; }

		public override IHeaderDictionary Headers { get; }

		public override IRequestCookieCollection Cookies { get; set; }
		public override long? ContentLength { get; set; }
		public override string ContentType { get; set; }
		public override Stream Body { get; set; }

		public override bool HasFormContentType { get; }

		public override IFormCollection Form { get; set; }

		public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
	public class FakeHttpResponse : HttpResponse
	{
		public override HttpContext HttpContext { get; }

		public override int StatusCode { get; set; }

		public override IHeaderDictionary Headers { get; }

		public FakeHttpResponse(IHeaderDictionary headers)
		{
			Headers = headers;
		}
		public FakeHttpResponse()
		{

		}

		public override Stream Body { get; set; }
		public override long? ContentLength { get; set; }
		public override string ContentType { get; set; }

		public override IResponseCookies Cookies { get; }

		public override bool HasStarted { get; }

		public override void OnCompleted(Func<object, Task> callback, object state)
		{
			throw new NotImplementedException();
		}

		public override void OnStarting(Func<object, Task> callback, object state)
		{
			throw new NotImplementedException();
		}

		public override void Redirect(string location, bool permanent)
		{
			throw new NotImplementedException();
		}
	}

}
