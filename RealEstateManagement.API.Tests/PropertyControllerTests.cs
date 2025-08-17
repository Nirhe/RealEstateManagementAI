using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.API.Data;
using RealEstateManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RealEstateManagement.API.Tests;

public class PropertyControllerTests
{
    private static RealEstateContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<RealEstateContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new RealEstateContext(options);
    }

    [Fact]
    public async Task GetById_ExistingProperty_ReturnsProperty()
    {
        using var context = CreateContext();
        var property = new Property { Address = "123 Main St", Bedrooms = 3, MonthlyRent = 1000m, PurchasePrice = 100000m };
        context.Properties.Add(property);
        await context.SaveChangesAsync();
        var controller = new PropertyController(context);

        var result = await controller.GetById(property.Id);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<Property>(okResult.Value);
        Assert.Equal(property.Address, returned.Address);
    }

    [Fact]
    public async Task GetById_UnknownProperty_ReturnsNotFound()
    {
        using var context = CreateContext();
        var controller = new PropertyController(context);

        var result = await controller.GetById(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ValidProperty_ReturnsCreated()
    {
        using var context = CreateContext();
        var controller = new PropertyController(context);
        var property = new Property { Address = "456 Elm St", Bedrooms = 2, MonthlyRent = 800m, PurchasePrice = 75000m };

        var result = await controller.Create(property);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdProperty = Assert.IsType<Property>(created.Value);
        Assert.Equal(property.Address, createdProperty.Address);
        Assert.Equal(1, await context.Properties.CountAsync());
    }
}
