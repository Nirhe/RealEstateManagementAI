using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.API.Models;
using System.Collections.Generic;
using Xunit;

namespace RealEstateManagement.API.Tests;

public class AcquisitionControllerTests
{
    [Fact]
    public void GetAll_ReturnsOkWithEmptyList()
    {
        // Arrange
        var controller = new AcquisitionController();

        // Act
        ActionResult<IEnumerable<InvestmentOpportunity>> result = controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var opportunities = Assert.IsType<List<InvestmentOpportunity>>(okResult.Value);
        Assert.Empty(opportunities);
    }
}
