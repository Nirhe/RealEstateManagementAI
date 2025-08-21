using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.API.Data;
using RealEstateManagement.API.Models;
using System.Threading.Tasks;
using Xunit;

namespace RealEstateManagement.API.Tests
{
    public class PropertyControllerTests
    {
        private DbContextOptions<RealEstateContext> _dbOptions;

        public PropertyControllerTests()
        {
            _dbOptions = new DbContextOptionsBuilder<RealEstateContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetById_ShouldReturnProperty_WhenPropertyExists()
        {
            // Arrange
            var property = new Property { Id = 1, Address = "123 Main St", Bedrooms = 3, MonthlyRent = 2000, PurchasePrice = 500000 };
            
            await using (var context = new RealEstateContext(_dbOptions))
            {
                context.Properties.Add(property);
                await context.SaveChangesAsync();
            }

            await using (var context = new RealEstateContext(_dbOptions))
            {
                var controller = new PropertyController(context);

                // Act
                var result = await controller.GetById(1);

                // Assert
                var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
                var returnedProperty = actionResult.Value.Should().BeOfType<Property>().Subject;
                returnedProperty.Should().BeEquivalentTo(property);
            }
        }
    }
}