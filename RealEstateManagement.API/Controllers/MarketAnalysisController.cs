using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Models;

namespace RealEstateManagement.API.Controllers;

/// <summary>
/// Endpoints for basic market analysis and ROI forecasting.  These endpoints perform simple
/// calculations and can be extended to call external services or machine learning models.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MarketAnalysisController : ControllerBase
{
    /// <summary>
    /// Calculate a simple cap rate for a property.  Supply the purchase price and expected
    /// monthly rent.  All values are placeholders and do not constitute financial advice.
    /// </summary>
    /// <param name="purchasePrice">Purchase price of the property.</param>
    /// <param name="monthlyRent">Expected monthly rent.</param>
    [HttpGet("roi")]
    public ActionResult<MarketAnalysisResult> Calculate(decimal purchasePrice, decimal monthlyRent)
    {
        if (purchasePrice <= 0)
        {
            return BadRequest("Purchase price must be greater than zero.");
        }
        if (monthlyRent < 0)
        {
            return BadRequest("Monthly rent must be non-negative.");
        }
        // Simple cap rate = (annual rent) / purchase price
        var annualRent = monthlyRent * 12;
        var capRate = purchasePrice == 0 ? 0 : annualRent / purchasePrice;
        var result = new MarketAnalysisResult
        {
            PurchasePrice = purchasePrice,
            MonthlyRent = monthlyRent,
            CapRate = capRate
        };
        return Ok(result);
    }
}