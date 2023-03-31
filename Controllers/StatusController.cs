using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BachelorOppgaveBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("post/{id}")]
        public IActionResult GetStatus([FromHeader] Guid userId, Guid id)
        {
            var user = _context.Users.Where(u => u.Id == userId).Include(u => u.UserRole).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            var post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }

            var status = _context.Statuses.Where(s => s.Id == post.StatusId).FirstOrDefault();
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }


        [HttpPost]
        public IActionResult EditStatus([FromHeader] Guid userId, [FromForm] Guid postId, [FromForm] string? type, [FromForm] string? description)
        {
            var user = _context.Users.Where(u => u.Id == userId).Include(u => u.UserRole).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }

            var status = _context.Statuses.Where(s => s.Id == post.StatusId).FirstOrDefault();
            if (status == null)
            {
                return NotFound();
            }

            if (user.UserRole.Type == "Admin")
            {
                if (!String.IsNullOrEmpty(type))
                {
                    status.Type = type;
                }
                
                status.Description = description;
                status.UserId = userId;
                _context.Statuses.Update(status);

                _context.SaveChanges();
                return Ok();
            }

            return Unauthorized();
            
        }

    }
}