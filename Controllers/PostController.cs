using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Mvc;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace BachelorOppgaveBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetPosts()
    {
        var posts = _context.Posts.Include(u => u.User)
            .Include(p => p.Category)
            .Include(a => a.Status)
            .ToList();

        if (posts == null)
        {
            return NotFound();
        }

        return Ok(posts);
    }
}
