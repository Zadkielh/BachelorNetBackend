using BachelorOppgaveBackend.Model;
using BachelorOppgaveBackend.PostgreSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BachelorOppgaveBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void recComments(Comment com, List<Comment> list) 
        {
            var sub = _context.Comments.Where(c => c.ParentComment == com).ToList();
            if (sub != null)
            {
                foreach (var comment in sub)
                {
                    if (!list.Contains(comment)) // Does list contain bottom comment?
                    {
                        if (com != null)
                        {
                            if (list.Contains(com)) // Check if parent exists, and if exists check if within list.
                            {
                                list.Remove(com); // Add comments to list.
                            }
                            list.Add(comment);
                        }

                    }
                    recComments(comment, list);

                }
            } 
        }

        [HttpGet]
        public IActionResult GetCommentsFromPost([FromHeader] Guid userId, [FromHeader] Guid postId)
        {
            List<Comment> comments = new List<Comment>();
            //var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            var parentComments = _context.Comments.Where(c => c.PostId == postId).Where(c => c.ParentComment == null).ToList();
            if (parentComments == null)
            {
                return NotFound("No comments found.");
            }
            comments.AddRange(parentComments);

            foreach (var comment in parentComments)
            {
                recComments(comment, comments);
            }
            
            
            return Ok(comments);
        }


        [HttpPost]
        public IActionResult AddComment([FromHeader][Required] Guid userId, [FromHeader][Required] Guid postId, [FromHeader] Guid? parentId, [FromForm][Required] string content)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null) { return NotFound("Invalid User"); }
            
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null) { return NotFound("Invalid Post"); }

            var comment = new Comment(post, user, null, content);

            var parentComment = _context.Comments.Where(c => c.Id == parentId).FirstOrDefault();
            if (parentComment != null) 
            {
                comment.ParentComment = parentComment;
                comment.ParentCommentId = parentId;
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }


        [HttpPut("/{id}")]
        public IActionResult EditComment([FromHeader][Required] Guid userId, Guid id, [FromForm][Required] string content)
        {

            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null) { return NotFound("Invalid User"); }

            var comment = _context.Comments.Where(c => c.Id == id).FirstOrDefault();
            if (comment == null) { return NotFound("Invalid Comment");  }

            if (comment.UserId != user.Id) { return Unauthorized(); }

            comment.Content = content;
            comment.Created = DateTime.UtcNow;

            _context.Comments.Update(comment);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete("/{id}")]
        public IActionResult DeleteComment([FromHeader][Required] Guid userId, Guid id)
        {
            var user = _context.Users.Where(u => u.Id == userId).Include(u => u.UserRole).FirstOrDefault();
            if (user == null) { return NotFound("Invalid User"); }

            var comment = _context.Comments.Where(c => c.Id == id).FirstOrDefault();
            if (comment == null) { return NotFound("Invalid Comment"); }

            if (comment.UserId != user.Id || user.UserRole.Type != "Admin") { return Unauthorized();  }

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return Ok();
        }
    }
}
