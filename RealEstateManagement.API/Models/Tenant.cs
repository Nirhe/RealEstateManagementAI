namespace RealEstateManagement.API.Models;

/// <summary>
/// Represents a tenant and their lease details.
/// </summary>
public class Tenant
{
    /// <summary>
    /// Unique identifier for the tenant.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tenant's full name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email address for sending notifications.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Identifier for the property the tenant rents.
    /// </summary>
    public int PropertyId { get; set; }

    /// <summary>
    /// Lease start date.
    /// </summary>
    public DateTime LeaseStart { get; set; }

    /// <summary>
    /// Lease end date.
    /// </summary>
    public DateTime LeaseEnd { get; set; }

    /// <summary>
    /// Monthly rent for the lease.
    /// </summary>
    public decimal MonthlyRent { get; set; }
}