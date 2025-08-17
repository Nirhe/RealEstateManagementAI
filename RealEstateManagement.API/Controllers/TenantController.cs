using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Models;

namespace RealEstateManagement.API.Controllers;

/// <summary>
/// Provides endpoints for managing tenants and leases.  Endpoints currently return
/// placeholder data and should be extended with real persistence.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    /// <summary>
    /// Retrieve all tenants.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Tenant>> GetAll()
    {
        // TODO: replace with data access logic
        var tenants = new List<Tenant>();
        return Ok(tenants);
    }

    /// <summary>
    /// Add a new tenant record.
    /// </summary>
    [HttpPost]
    public ActionResult<Tenant> Create([FromBody] Tenant tenant)
    {
        // TODO: persist tenant
        tenant.Id = 1; // placeholder ID assignment
        return CreatedAtAction(nameof(GetAll), new { id = tenant.Id }, tenant);
    }
}