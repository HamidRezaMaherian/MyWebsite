using MyWebsite.Presentation.IntegrationTests.Utils;
using System.Net;

namespace MyWebsite.Presentation.IntegrationTests
{
   [TestFixture]
   public class HealthCheckTests
   {
      [Test]
      public async Task HealthCheckEndpoint_ReturnsOk()
      {
         var application = new TestingWebAppFactory<Program>();
         var client = application.CreateClient();
         var result = await client.GetAsync("/healthstatus");
         Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      }
   }
}
