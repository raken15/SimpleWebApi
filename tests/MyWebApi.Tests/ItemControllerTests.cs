using Microsoft.AspNetCore.Mvc;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.Models;
using Moq;

namespace MyWebApi.Tests;

public class ItemControllerTests
{
    [Fact]
    public void GetItem_ReturnsOk_WhenItemExists()
    {
        // Arrange
        var mockRepository = new Mock<IItemRepository>();
        var itemId = 1;
        var expectedItem = new Item(itemId, "Test Item", 9.99m, DateTime.Now);
        mockRepository.Setup(repo => repo.GetItem(itemId)).Returns(expectedItem);

        var controller = new ItemController(mockRepository.Object);

        // Act
        var result = controller.GetItem(itemId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedItem, result.Value);
    }
}