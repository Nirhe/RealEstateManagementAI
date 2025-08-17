using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.API.Models;
using Xunit;

namespace RealEstateManagement.API.Tests;

public class MarketAnalysisControllerTests
{
    [Fact]
    public void Calculate_ValidInputs_ReturnsOkWithExpectedCapRate()
    {
        // Arrange
        var controller = new MarketAnalysisController();
        decimal purchasePrice = 100000m;
        decimal monthlyRent = 1000m;
        decimal expectedCapRate = (monthlyRent * 12) / purchasePrice;

        // Act
        ActionResult<MarketAnalysisResult> actionResult = controller.Calculate(purchasePrice, monthlyRent);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var resultValue = Assert.IsType<MarketAnalysisResult>(okResult.Value);
        Assert.Equal(expectedCapRate, resultValue.CapRate);
    }

    [Fact]
    public void Calculate_NegativeMonthlyRent_ReturnsBadRequest()
    {
        // Arrange
        var controller = new MarketAnalysisController();
        decimal purchasePrice = 100000m;
        decimal monthlyRent = -500m;

        // Act
        ActionResult<MarketAnalysisResult> actionResult = controller.Calculate(purchasePrice, monthlyRent);

        // Assert
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }
}
