using Docker.DotNet.Models;
using MyWardrobe.Contracts.WardrobeItem;
using System.Drawing;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using MyWardrobe.ErrorHandling;

namespace MyWardrobe.IntegrationTests
{
    public class WardrobeItemIntegrationTests : BaseIntegrationTest
    {
        public WardrobeItemIntegrationTests(MsSqlContainerFixture fixture)
            : base(fixture)
        {}

        #region CREATE

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
            };

            // Act
            var createNewItem = await Client.PostAsJsonAsync(BaseUri, newItem);

            // Assert
            createNewItem.EnsureSuccessStatusCode();
            var items = await Client.GetFromJsonAsync<WardrobeItemResponse[]>(BaseUri);

            Assert.NotNull(items);
            Assert.Contains(items, i => 
                i.Category == newItem.Category &&
                i.Subcategory == newItem.SubCategory &&
                i.Brand == newItem.Brand &&
                i.Model == newItem.Model
            );
        }

        [Fact]
        public async Task CreatNewItemWithoutCategory_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidItem = new
            {
                SubCategory = "Sneaker",
                Brand = "Adidas",
                Model = "Campus"
            };

            // Act
            var createInvalidItem = await Client.PostAsJsonAsync(BaseUri, invalidItem);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, createInvalidItem.StatusCode);
        }

        #endregion

        #region GET

        [Fact]
        public async Task GetSingleItem_ShouldReturnItem()
        {
            // Arrange
            var newItem = new
            {
                Category = "Skor",
                SubCategory = "Löparskor",
                Brand = "New Balance",
                Model = "880"
            };

            var createNewItem = await Client.PostAsJsonAsync(BaseUri, newItem);
            createNewItem.EnsureSuccessStatusCode();
            var getNewItem = await createNewItem.Content.ReadFromJsonAsync<WardrobeItemResponse>();


            // Act
            var item = await Client.GetFromJsonAsync<WardrobeItemResponse>($"{BaseUri}/{getNewItem.Id}");

            // Assert
            Assert.NotNull(item);
            Assert.Equal(getNewItem.Id, item.Id);
            Assert.Equal(newItem.Category, item.Category);
            Assert.Equal(newItem.SubCategory, item.Subcategory);
            Assert.Equal(newItem.Brand, item.Brand);
            Assert.Equal(newItem.Model, item.Model);

        }

        [Fact]
        public async Task GetAllItems_ShouldReturnItems()
        {
            // Arrange
            var newItem = new
            {
                Category = "Byxor",
                Subcategory = "Jeans",
                Brand = "Levis",
                Model = "501"
            };

            var createNewItem = await Client.PostAsJsonAsync(BaseUri, newItem);
            createNewItem.EnsureSuccessStatusCode();

            // Act
            var items = await Client.GetFromJsonAsync<WardrobeItemResponse[]>(BaseUri);

            // Assert
            Assert.NotNull(items);
            Assert.NotEmpty(items);
            Assert.Contains(items, i =>
                i.Category == newItem.Category &&
                i.Subcategory == newItem.Subcategory &&
                i.Brand == newItem.Brand &&
                i.Model == newItem.Model
            );
        }

        #endregion

        #region UPDATE

        [Fact]
        public async Task UpdateExistingItem_ShouldModifyItemInDatabase()
        {
            // Arrange
            var newItem = new
            {
                Category = "Skor",
                Subcategory = "Löparskor",
                Brand = "New Balance",
                Model = "880",
                Price = 1500
            };

            var createNewItem = await Client.PostAsJsonAsync(BaseUri, newItem);
            createNewItem.EnsureSuccessStatusCode();

            var items = await Client.GetFromJsonAsync<WardrobeItemResponse[]>(BaseUri);
            var item = items!.First();

            var updateRequest = new
            {
                Category = "Skor",
                Subcategory = "Löparskor",
                Brand = "New Balance",
                Model = "880",
                Price = 1600,
            };

            // Act
            var updateResponse = await Client.PutAsJsonAsync($"{BaseUri}/{item.Id}", updateRequest);

            // Assert
            updateResponse.EnsureSuccessStatusCode();
            var updatedItem = await Client.GetFromJsonAsync<WardrobeItemResponse>($"{BaseUri}/{item.Id}");
            Assert.Equal(updateRequest.Price, updatedItem.Price);
        }

        #endregion


    }
}                   // Bryt ut upprepad kod - newItem etc. se best practice för test hos microsoft