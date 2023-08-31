using CodeChallenge.Context;
using CodeChallenge.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System.Data.Common;
using TestForChallenge;

public class UnitOfWorkTests
{
    [Fact]
    public void CreateOrderWithTotalCost_ValidInput_CommitsTransaction()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CodeContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new CodeContext(options);
        var dbContextMock = new Mock<CodeContext>(new DbContextOptions<CodeContext>());

        var unitOfWork = new Mock<UnitOfWork>(context);
        var customerId = 1;
        var productId = 2;
        var quantity = 3;

        
        unitOfWork.Setup(x => x.CreateOrderWithTotalCost(customerId, productId, quantity)).Returns(true);
    
    }


}
