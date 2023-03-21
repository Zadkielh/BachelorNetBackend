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
    public IActionResult GetUser()
    {
        var users = _context.Users.Include(r => r.UserRole).ToList();
        if (users == null)
        {
            return NotFound();
        }
        return Ok(users);
    }
    
    
    [HttpPost]
    public IActionResult PostUser([FromForm] Guid azureId, [FromForm] string userName, [FromForm] string userEmail, [FromHeader] string token)
    {
        var secretKey = "1234";
        if (token != secretKey)
        {
            return NotFound("Invalid token");
        }

        var userExists = _context.Users.Include(u => u.UserRole).Where(u => u.AzureId == azureId).FirstOrDefault();

        if(userExists != null)
        {
            return Ok(userExists);
        }
        var role = _context.UsersRoles.Where(r => r.Type == "User").FirstOrDefault();

        if (role == null) return NotFound("Missing Role");

        var user = new User(role, azureId, userName, userEmail);

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(user);
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