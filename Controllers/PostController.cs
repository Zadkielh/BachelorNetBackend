using System.Diagnostics.Metrics;
using System.Runtime.InteropServices.ComTypes;
using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Mvc;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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
    public IActionResult GetPosts(string? title, string? category)
    {
        IQueryable<Post> posts = _context.Set<Post>();
        
        if (category != null)
        {
            posts = posts.Where(p => p.Category.Type == category);
        }
        if (title != null)
        {
            posts = posts.Where(p => p.Title.Contains(title));
        }
        
        var res = posts.Select(p => new
        {
            p.Id,
            p.Title,
            p.Description,
            p.Created,
            votes = _context.Votes.Count(v => p.Id == v.PostId),
            user = new { p.UserId, p.User.UserName, p.User.Email },
            category = new { p.CategoryId, p.Category.Type },
            status = new { p.StatusId, p.Status.Type }
        }).ToList();
        
        if (res == null)
        {
            return NotFound();
        }
        
        return Ok(res);
    }

   

    [HttpGet("id/{id}")]
    public IActionResult GetPostById(Guid id)
    {
        var posts = _context.Posts
            .Select(p => new
            {
                p.Id,
                p.Title,
                p.Description,
                p.Created,
                votes = _context.Votes.Count(v => p.Id == v.PostId),
                user = new { p.UserId, p.User.UserName, p.User.Email },
                category = new { p.CategoryId, p.Category.Type },
                status = new { p.StatusId, p.Status.Type }
            })
            .Where(t => t.Id == id)
            .FirstOrDefault();

        if (posts == null)
        {
            return NotFound();
        }

        return Ok(posts);
    }


    [HttpPost]
    public IActionResult AddPost([FromHeader] Guid userId, [FromForm] FormPost post)
    {
        var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        if (user == null)
        {
            return NotFound("Invalid user");
        }

        var category = _context.Categories.Where(c => c.Id == post.categoryId).FirstOrDefault();
        if (category == null)
        {
            return NotFound("Invaild category");
        }

        var s = new Status(null, "Venter", "Venter på svar");
        var p = new Post(user, category, s, post.title ?? "", post.description ?? "");
        _context.Posts.Add(p);
        _context.SaveChanges();
        return Ok();
    }
    

    [HttpDelete("id/{id}")]
    public IActionResult DeletePost([FromHeader] Guid userId, Guid id)
    {
        var user = _context.Users.Where(u => u.Id == userId).Include(u => u.UserRole).FirstOrDefault();
        if (user == null)
        {
            return NotFound();
        }

        var post = _context.Posts.Where(p => p.Id == id).Include(p => p.User).FirstOrDefault();
        if (post == null)
        {
            return NotFound();
        }

        if (user.Id == post.UserId || user.UserRole.Type == "Admin")
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return Ok();
        }
        
        return NotFound("Invalid access");
    }
}