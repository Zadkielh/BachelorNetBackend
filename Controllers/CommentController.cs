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

        private List<CommentList> recComments(Guid c) 
        {   
            var parentComments = _context.Comments.Where(c => c.ParentCommentId == c.Id).
              Select(c => new CommentList
            {
                Id = c.Id,
                Content = c.Content,
                ParentCommentId = c.ParentCommentId
            }).ToList();

            if (parentComments.Count() == 0) {
                return new List<CommentList>();
            }

             for (int i = 0; i < parentComments.Count(); i++) {
                parentComments[i].ParentComment = recComments(parentComments[i].Id);
            }
            
            return parentComments;
        }

        [HttpGet]
        public IActionResult GetCommentsFromPost([FromHeader] Guid userId, [FromHeader] Guid postId)
        {
            //var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            var parentComments = _context.Comments.Include(p => p.ParentComment).Where(c => c.PostId == postId).Where(c => c.ParentCommentId == null).
            Select(c => new CommentList
            {
                Id = c.Id,
                Content = c.Content,
                ParentCommentId = c.ParentCommentId
            }).ToList();
            
             if (parentComments.Count() == 0)
            {
                return NotFound("No comments found.");
            }
            
            for (int i = 0; i < parentComments.Count(); i++) {
                parentComments[i].ParentComment = recComments(parentComments[i].Id);
            }

            return Ok(parentComments);
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
                comment.ParentCommentId= parentId;
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
