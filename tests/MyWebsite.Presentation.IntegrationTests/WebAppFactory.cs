using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MyWebsite.Presentation.IntegrationTests.Utils
{
	public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
	{
		private readonly Action<IServiceCollection> _mockConfigureServices;
		private readonly string environment;
		private IHost? _host;

		public string ServerAddress
		{
			get
			{
				EnsureServer();
				return ClientOptions.BaseAddress.ToString();
			}
		}
		protected override IHost CreateHost(IHostBuilder builder)
		{
			// Create the host for TestServer now before we
			// modify the builder to use Kestrel instead.
			var testHost = builder.Build();

			// Modify the host builder to use Kestrel instead
			// of TestServer so we can listen on a real address.
			builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel((cfg) =>
			{
				Random rand = new();
				cfg.ListenLocalhost(rand.Next(6000, 7000));
			}));

			// Create and start the Kestrel server before the test server,
			// otherwise due to the way the deferred host builder works
			// for minimal hosting, the server will not get "initialized
			// enough" for the address it is listening on to be available.
			// See https://github.com/dotnet/aspnetcore/issues/33846.
			_host = builder.Build();
			_host.Start();

			// Extract the selected dynamic port out of the Kestrel server
			// and assign it onto the client options for convenience so it
			// "just works" as otherwise it'll be the default http://localhost
			// URL, which won't route to the Kestrel-hosted HTTP server.
			var server = _host.Services.GetRequiredService<IServer>();
			var addresses = server.Features.Get<IServerAddressesFeature>();
			ClientOptions.BaseAddress = addresses!.Addresses
				 .Select(x => new Uri(x))
				 .Last();


			// Return the host that uses TestServer, rather than the real one.
			// Otherwise the internals will complain about the host's server
			// not being an instance of the concrete type TestServer.
			// See https://github.com/dotnet/aspnetcore/pull/34702.
			testHost.Start();
			return testHost;
		}

		public TestingWebAppFactory(Action<IServiceCollection> mockConfigureServices)
		{
			_mockConfigureServices = mockConfigureServices;
		}
		public TestingWebAppFactory(string environment)
		{
			this.environment = environment;
		}
		private void EnsureServer()
		{
			if (_host is null)
			{
				// This forces WebApplicationFactory to bootstrap the server  
				using var _ = CreateDefaultClient();
			}
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
		protected override void Dispose(bool disposing)
		{
			_host!.StopAsync().Wait();
			_host!.Dispose();
			base.Dispose(disposing);
		}
		public override async ValueTask DisposeAsync()
		{
			await _host!.StopAsync();
			_host!.Dispose();
			await base.DisposeAsync();
		}
	}
}
