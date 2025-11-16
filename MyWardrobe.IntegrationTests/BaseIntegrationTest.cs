using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWardrobe.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<MsSqlContainerFixture>
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        protected readonly HttpClient Client;
        protected readonly string BaseUri;
        protected readonly string BaseUriItemUsage;

        protected BaseIntegrationTest(MsSqlContainerFixture fixture)
        {
            Factory = new IntegrationTestWebAppFactory(fixture.ConnectionString);
            Client = Factory.CreateClient();
            BaseUri = "/api/wardrobe-items";
            BaseUriItemUsage = "/api/wardrobe-items-usage";
        }

    }
}
