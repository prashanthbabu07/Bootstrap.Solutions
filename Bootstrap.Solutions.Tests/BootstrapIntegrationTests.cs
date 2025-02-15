using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Bootstrap.Solutions.Tests;

public class BootstrapWebApplicationFactory : WebApplicationFactory<Web.Api.Program>
{
}

public class TestHttpClientFixture : IDisposable
{
	public readonly BootstrapWebApplicationFactory Factory;
	public readonly HttpClient Client;

	public TestHttpClientFixture()
	{
		Factory = new BootstrapWebApplicationFactory();
		Client = Factory.CreateClient();
	}

	public void Dispose()
	{
		Client.Dispose();
		Factory.Dispose();
	}
}

[CollectionDefinition("API Test Collection")]
public class ApiTestCollection : ICollectionFixture<TestHttpClientFixture> { }

