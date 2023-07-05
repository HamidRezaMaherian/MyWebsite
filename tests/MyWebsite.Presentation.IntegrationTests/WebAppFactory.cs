using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace MyWebsite.Presentation.IntegrationTests.Utils
{
	public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
	{
		private readonly Action<IServiceCollection> _mockConfigureServices;

		public TestingWebAppFactory(Action<IServiceCollection> mockConfigureServices)
		{
			_mockConfigureServices = mockConfigureServices;
		}
		public TestingWebAppFactory()
		{
			_mockConfigureServices = null;
		}
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			if (_mockConfigureServices != null)
				builder.ConfigureServices(_mockConfigureServices);
			else
				base.ConfigureWebHost(builder);
		}
	}
}
