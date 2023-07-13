using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyWebsite.Presentation.Admin.Shared;
using MyWebsite.Presentation.UnitTests.Utils;

namespace MyWebsite.Presentation.Tests.Unit
{
	[TestFixture]
	public class BaseComponentTests : BunitTestContext
	{
		private IHttpContextAccessor _accessor;

		[SetUp]
		public void SetUp()
		{
			Setup();
			_accessor = CreateHttpCtxAccessor();
			TestContext!.Services.Replace(new ServiceDescriptor(typeof(IHttpContextAccessor), _accessor));
		}
		[Test]
		public void CreateCmp()
		{
			var cut = TestContext!.RenderComponent<FakeBaseComponent>();
			Assert.That(cut.Instance, Is.Not.Null);
		}
		[Test]
		public void PassNullParameters()
		{
			TestContext!.Services.RemoveAll(typeof(IHttpContextAccessor));
			Assert.Throws<InvalidOperationException>(() =>
			{
				var renderedCmp = TestContext!.RenderComponent<FakeBaseComponent>();
			});
		}
		[Test]
		public void AddCmpNameHeader()
		{
			TestContext!.RenderComponent<FakeBaseComponent>();
			var header = new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("X-Component", "FakeBase");
			Assert.That(_accessor.HttpContext!.Response.Headers.Contains(header), Is.True);
		}
		#region Privates
		private IHttpContextAccessor CreateHttpCtxAccessor()
		{
			return new HttpContextAccessor()
			{
				HttpContext = CreateContext()
			};
		}
		private HttpContext CreateContext()
		{
			var req = new FakeHttpRequest();
			var res = new FakeHttpResponse(
				new HeaderDictionary(
					new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()
					)
				);
			return new FakeHttpContext(req, res);
		}
		#endregion

		public class FakeBaseComponent : BaseComponent { }
	}
}
