using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.API.Data;
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
    private readonly RealEstateContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TenantController"/> class.
    /// </summary>
    public TenantController(RealEstateContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieve all tenants.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tenant>>> GetAll()
    {
        var tenants = await _context.Tenants.ToListAsync();
        return Ok(tenants);
    }

    /// <summary>
    /// Add a new tenant record.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Tenant>> Create([FromBody] Tenant tenant)
    {
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = tenant.Id }, tenant);
    }
}