using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Mvc;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace BachelorOppgaveBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var users = _context.Users.Include(r => r.UserRole).ToList();
        if (users == null)
        {
            return NotFound();
        }
        return Ok(users);
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