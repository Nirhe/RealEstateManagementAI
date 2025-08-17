using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Models;

namespace RealEstateManagement.API.Controllers;

/// <summary>
/// Provides endpoints for managing properties.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    // In-memory store for demo purposes
    private static readonly List<Property> _properties = new();
    private static int _nextId = 1;

    /// <summary>
    /// Retrieve all properties in the portfolio.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Property>> GetAll()
    {
        return Ok(_properties);
    }

    /// <summary>
    /// Add a new property to the portfolio.
    /// </summary>
    [HttpPost]
    public ActionResult<Property> Create([FromBody] Property property)
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

        // Assign a new ID and add to the list
        property.Id = _nextId++;
        _properties.Add(property);

        return CreatedAtAction(nameof(GetAll), new { id = property.Id }, property);
    }
}