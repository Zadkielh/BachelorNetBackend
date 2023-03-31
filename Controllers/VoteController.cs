using BachelorOppgaveBackend.Model;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BachelorOppgaveBackend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class VoteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("user")]
        public IActionResult GetUserVotes([FromHeader] Guid userId)
        {
            var votes = _context.Votes.Where(u => u.UserId == userId).ToList();
            if (votes == null)
            {
                return NotFound();
            }
            return Ok(votes);
        }

        [HttpGet("post")]
        public IActionResult GetPostVotes([FromHeader] Guid userId, Guid postId)
        {
            var user = _context.Users.Where(u => u.Id == userId).Include(u => u.UserRole).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }

            var post = _context.Posts.Where(p => p.Id == postId).Include(p => p.User).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }

            if (user.Id == post.UserId || user.UserRole.Type == "Admin")
            {
                var votes = _context.Votes.Where(u => u.PostId == postId).ToList();
                if (votes == null)
                {
                    return NotFound();
                }
                return Ok(votes);
            }
            
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult Vote([FromHeader] Guid userId, [FromForm] int direction, [FromForm] Guid postId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Invalid user");
            }

            var post = _context.Posts.Where(c => c.Id == postId).FirstOrDefault();
            if (post == null)
            {
                return NotFound("Invalid post");
            }

            var vote = _context.Votes.Where(u => u.PostId == postId).Where(u => u.UserId == userId).FirstOrDefault();

            var s = new Status(Guid.Empty, "Venter", "Venter på svar");

            Vote p;
            switch(direction)
            {
                case 1:
                    if (vote != null)
                    {
                        vote.Liked = true;
                        _context.Votes.Update(vote);
                        break;
                    }

                    p = new Vote(user.Id, post.Id, true);
                    _context.Votes.Add(p);
                    break;
                case 0:

                    if (vote == null)
                    {
                        return NotFound("Invalid Vote");
                    }

                    _context.Votes.Remove(vote);
                    break;
                case -1:

                    if (vote != null)
                    {
                        vote.Liked = false;
                        _context.Votes.Update(vote);
                        break;
                    }

                    p = new Vote(user.Id, post.Id, false);
                    _context.Votes.Add(p);
                    break;
            }
            
            _context.SaveChanges();
            return Ok();
        }

    }
}