using MyWardrobe.Contracts.WardrobeItem;
using System.Net.Http.Json;
using Xunit; 

namespace MyWardrobe.IntegrationTests
{
    public class WardrobeItemIntegrationTests : BaseIntegrationTest
    {
        public WardrobeItemIntegrationTests(MsSqlContainerFixture fixture)
            : base(fixture)
        {}

        [Fact]
        public async Task CreateNewItemWithRequiredProperties_ShouldAddItemToDatabase()
        {
            // Arrange
            var newItem = new
            {
                Category = "Skor",
                SubCategory = "Sneakers",
                Brand = "Adidas",
                Model = "Handball",
                Price = 1200
            };

            // Act
            var createNewItem = await Client.PostAsJsonAsync(BaseUri, newItem);

            // Assert
            createNewItem.EnsureSuccessStatusCode();
            var items = await Client.GetFromJsonAsync<WardrobeItemResponse[]>(BaseUri);

            Assert.NotNull(items);
            Assert.Contains(items, i => i.Category == newItem.Category);
            Assert.Contains(items, i => i.Subcategory == newItem.SubCategory);
            Assert.Contains(items, i => i.Brand == newItem.Brand);
            Assert.Contains(items, i => i.Model == newItem.Model);
            Assert.Contains(items, i => i.Price == newItem.Price);
        }
    }
}