using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Mvc;
using BachelorOppgaveBackend.PostgreSQL;

namespace BachelorOppgaveBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    [HttpGet]
    public IActionResult Get()
    {
        var categories = _context.Categories.ToList();
        if (categories == null)
        {
            return NotFound();
        }
        return Ok(categories);
    }
    
    
    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }

    
    [HttpPut]
    public IActionResult Put()
    {
        return Ok();
    }

    
    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok();
    }

}