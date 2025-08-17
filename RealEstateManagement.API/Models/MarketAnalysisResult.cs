namespace RealEstateManagement.API.Models;

/// <summary>
/// A simple DTO for returning market analysis calculations.  Additional fields can
/// be added as more sophisticated metrics are introduced.
/// </summary>
public class MarketAnalysisResult
{
    public decimal PurchasePrice { get; set; }
    public decimal MonthlyRent { get; set; }
    public decimal CapRate { get; set; }
}