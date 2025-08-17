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

public class TenantControllerTests
{
    private static RealEstateContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<RealEstateContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new RealEstateContext(options);
    }

    [Fact]
    public async Task GetAll_ReturnsTenants()
    {
        using var context = CreateContext();
        context.Tenants.Add(new Tenant
        {
            Name = "John Doe",
            Email = "john@example.com",
            PropertyId = 1,
            LeaseStart = DateTime.UtcNow,
            LeaseEnd = DateTime.UtcNow.AddMonths(12),
            MonthlyRent = 1200m
        });
        await context.SaveChangesAsync();
        var controller = new TenantController(context);

        var result = await controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var tenants = Assert.IsType<List<Tenant>>(okResult.Value);
        Assert.Single(tenants);
    }

    [Fact]
    public async Task Create_ValidTenant_ReturnsCreated()
    {
        using var context = CreateContext();
        var controller = new TenantController(context);
        var tenant = new Tenant
        {
            Name = "Jane Smith",
            Email = "jane@example.com",
            PropertyId = 1,
            LeaseStart = DateTime.UtcNow,
            LeaseEnd = DateTime.UtcNow.AddMonths(6),
            MonthlyRent = 1000m
        };

        var result = await controller.Create(tenant);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdTenant = Assert.IsType<Tenant>(created.Value);
        Assert.Equal(tenant.Name, createdTenant.Name);
        Assert.Equal(1, await context.Tenants.CountAsync());
    }
}
