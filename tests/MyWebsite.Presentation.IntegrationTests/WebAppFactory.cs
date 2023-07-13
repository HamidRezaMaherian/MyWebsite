using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace MyWebsite.Presentation.IntegrationTests.Utils
{
	public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
	{
		private readonly Action<IServiceCollection> _mockConfigureServices;
		private readonly string environment;

		public TestingWebAppFactory(Action<IServiceCollection> mockConfigureServices)
		{
			_mockConfigureServices = mockConfigureServices;
		}
		public TestingWebAppFactory(string environment)
		{
			this.environment = environment;
		}
		public TestingWebAppFactory(Action<IServiceCollection> mockConfigureServices, string environment)
		{
			_mockConfigureServices = mockConfigureServices;
			this.environment = environment;
		}
		public TestingWebAppFactory()
		{
			_mockConfigureServices = null;
		}
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			if (environment is not null)
				builder.UseEnvironment(environment);
			if (_mockConfigureServices != null)
				builder.ConfigureServices(_mockConfigureServices);
			else
				base.ConfigureWebHost(builder);
		}
	}
}
