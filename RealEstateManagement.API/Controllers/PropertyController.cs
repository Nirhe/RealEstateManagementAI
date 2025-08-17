using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.API.Data;
using RealEstateManagement.API.Models;

namespace RealEstateManagement.API.Controllers;

/// <summary>
/// Provides endpoints for managing properties.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly RealEstateContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyController"/> class.
    /// </summary>
    public PropertyController(RealEstateContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieve all properties in the portfolio.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetAll()
    {
        var properties = await _context.Properties.ToListAsync();
        return Ok(properties);
    }

    /// <summary>
    /// Retrieve a property by its unique identifier.
    /// </summary>
    /// <param name="id">The id of the property to retrieve.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Property>> GetById(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return NotFound();
        }

        return Ok(property);
    }

    /// <summary>
    /// Add a new property to the portfolio.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Property>> Create([FromBody] Property property)
    {
        if (property == null)
        {
            return BadRequest("Property object is null");
        }

        // Validate required fields
        if (string.IsNullOrWhiteSpace(property.Address))
        {
            return BadRequest("Address is required");
        }

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
    }

    /// <summary>
    /// Update an existing property.
    /// </summary>
    /// <param name="id">The id of the property to update.</param>
    /// <param name="property">The updated property details.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Property property)
    {
        if (property == null)
        {
            return BadRequest("Property object is null");
        }

        if (id != property.Id)
        {
            return BadRequest("Property ID mismatch");
        }

        if (string.IsNullOrWhiteSpace(property.Address))
        {
            return BadRequest("Address is required");
        }

        var existingProperty = await _context.Properties.FindAsync(id);
        if (existingProperty == null)
        {
            return NotFound();
        }

        existingProperty.Address = property.Address;
        existingProperty.Bedrooms = property.Bedrooms;
        existingProperty.MonthlyRent = property.MonthlyRent;
        existingProperty.PurchasePrice = property.PurchasePrice;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove a property from the portfolio.
    /// </summary>
    /// <param name="id">The id of the property to delete.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return NotFound();
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}