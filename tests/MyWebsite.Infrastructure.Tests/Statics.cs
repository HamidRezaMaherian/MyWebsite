namespace MyWebsite.Infrastructure.Tests;
public static class Statics
{
#if DEBUG
	public const string dbConnectionString = "Server=.;Database=mywebiste-testdb;Trusted_Connection=True;Trust Server Certificate=true";
#elif TEST
	public const string dbConnectionString = "Server=localhost;Database=mywebiste-testdb;User Id=SA;Password=Abcd1234@;Trusted_Connection=False;TrustServerCertificate=True;";
#endif
}