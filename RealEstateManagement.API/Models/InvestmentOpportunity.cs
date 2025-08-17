namespace RealEstateManagement.API.Models;

/// <summary>
/// Represents a prospective property for acquisition along with high‑level ROI estimates.
/// This type can be expanded to include additional analytics and AI‑derived metrics.
/// </summary>
public class InvestmentOpportunity
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public decimal AskingPrice { get; set; }
    public decimal EstimatedMonthlyRent { get; set; }
    public decimal CapRate { get; set; }
    public decimal CashOnCashReturn { get; set; }
}