using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Mvc;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
        var posts = _context.Posts
            .Select(p => new {
                p.Id,
                p.Title,
                p.Description,
                voteCount = 100,
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
        var posts = _context.Posts
            .Select(p => new {
                p.Id,
                p.Title,
                p.Description,
                voteCount = 100,
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
        
        // Loop true votes and count
        foreach (var pos in posts)
        {
            continue;
        }
        
        return Ok(posts);
    }

}
