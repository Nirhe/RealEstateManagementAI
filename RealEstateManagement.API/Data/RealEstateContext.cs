using Microsoft.EntityFrameworkCore;
using RealEstateManagement.API.Models;

namespace RealEstateManagement.API.Data;

/// <summary>
/// Entity Framework database context for real estate management.
/// </summary>
public class RealEstateContext : DbContext
{
    public RealEstateContext(DbContextOptions<RealEstateContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Rental properties tracked by the system.
    /// </summary>
    public DbSet<Property> Properties { get; set; } = null!;

    /// <summary>
    /// Tenants and their lease details.
    /// </summary>
    public DbSet<Tenant> Tenants { get; set; } = null!;
}
