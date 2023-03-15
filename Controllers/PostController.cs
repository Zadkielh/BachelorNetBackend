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
            .Select(p => new {
                p.Id,
                p.Title,
                p.Description,
                p.Created,
                user = new {p.UserId, p.User.UserName, p.User.Email},
                category = new {p.CategoryId, p.Category.Type},
                status = new {p.StatusId, p.Status.Type}
                
                })
            .ToList();

        if (posts == null)
        {
            return NotFound();
        }

        return Ok(posts);
    }

    [HttpGet("{title}")]
     public IActionResult GetPosts(string title)
    {
        var posts = _context.Posts.Include(u => u.User)
            .Select(p => new {
                p.Id,
                p.Title,
                p.Description,
                p.Created,
                user = new {p.UserId, p.User.UserName, p.User.Email},
                category = new {p.CategoryId, p.Category.Type},
                status = new {p.StatusId, p.Status.Type}
                
                })
            .Where(t => t.Title.Contains(title))
            .ToList();

        if (posts == null)
        {
            return NotFound();
        }

        return Ok(posts);
    }

}
