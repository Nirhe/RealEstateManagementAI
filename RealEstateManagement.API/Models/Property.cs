namespace RealEstateManagement.API.Models;

/// <summary>
/// Represents a rental property within the portfolio.
/// Additional fields such as taxes, HOA fees and maintenance costs can be added
/// as the model evolves.
/// </summary>
public class Property
{
    /// <summary>
    /// Unique identifier for the property.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Street address of the property.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Number of bedrooms.
    /// </summary>
    public int Bedrooms { get; set; }
    
    /// <summary>
    /// Monthly rent charged to tenants.
    /// </summary>
    public decimal MonthlyRent { get; set; }

    /// <summary>
    /// Purchase price of the property.
    /// </summary>
    public decimal PurchasePrice { get; set; }
}