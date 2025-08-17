using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Models;

namespace RealEstateManagement.API.Controllers;

/// <summary>
/// Provides endpoints for discovering and evaluating prospective real estate investments.
/// Currently returns an empty list of opportunities as a placeholder.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AcquisitionController : ControllerBase
{
    /// <summary>
    /// Retrieve a list of investment opportunities.  In a full implementation this
    /// would call out to external MLS services or screening engines.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<InvestmentOpportunity>> GetAll()
    {
        var opportunities = new List<InvestmentOpportunity>();
        return Ok(opportunities);
    }
}