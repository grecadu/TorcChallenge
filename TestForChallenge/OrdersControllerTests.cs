using CodeChallenge.Context;
using CodeChallenge.Controllers;
using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TestForChallenge;
using Xunit;

public class OrdersControllerTests
{
  
    [Fact]
    public void GetOrder_ExistingOrderId_ReturnsOkWithOrder()
    {
        var options = new DbContextOptionsBuilder<CodeContext>()
    .UseInMemoryDatabase(databaseName: "TestDatabase")
    .Options;

        var context = new Mock<CodeContext>(options);
        var loger = new Mock<ILogger<OrdersController>>();
        // Arrange
        var unitOfWorkMock = new Mock<UnitOfWork>(context.Object); // Mock your UnitOfWork
        var controller = new OrdersController(unitOfWorkMock.Object, loger.Object);

        var orderId = 1; // Replace with an existing order ID in your test data

        // Mock your UnitOfWork's OrderRepository to return a sample order


        unitOfWorkMock.Setup(uow => uow.OrderRepository.GetById(orderId))
            .Returns(new Order { Id = orderId, /* other properties */ });

        // Act
        var result = controller.GetOrder(orderId);

        // Assert
        Assert.IsType<OkObjectResult>(result);

        var okResult = (OkObjectResult)result;
        Assert.IsType<Order>(okResult.Value);
    }

    [Fact]
    public void GetOrder_NonExistingOrderId_ReturnsNotFound()
    {
        var options = new DbContextOptionsBuilder<CodeContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        var context = new Mock<CodeContext>(options);
        var loger = new Mock<ILogger<OrdersController>>();


        // Arrange
        var unitOfWorkMock = new Mock<UnitOfWork>(context.Object); // Mock your UnitOfWork
        var controller = new OrdersController(unitOfWorkMock.Object, loger.Object);

        var orderId = 999; // Replace with a non-existing order ID

        // Mock your UnitOfWork's OrderRepository to return null for a non-existing order
        unitOfWorkMock.Setup(uow => uow.OrderRepository.GetById(orderId))
            .Returns((Order)null);

        // Act
        var result = controller.GetOrder(orderId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
