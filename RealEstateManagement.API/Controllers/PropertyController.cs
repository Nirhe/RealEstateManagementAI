using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Models;
using System.Linq;

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
    /// Retrieve a property by its unique identifier.
    /// </summary>
    /// <param name="id">The id of the property to retrieve.</param>
    [HttpGet("{id}")]
    public ActionResult<Property> GetById(int id)
    {
        var property = _properties.FirstOrDefault(p => p.Id == id);
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

        return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
    }
}