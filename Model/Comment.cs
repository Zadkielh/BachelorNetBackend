using System.ComponentModel.DataAnnotations;

namespace BachelorOppgaveBackend.Model
{
    public class Comment
    {
        public Comment() {}

        public Comment(Post post, User user, Comment subComment, string content)
        {
            Post = post;
            User = user;
            SubComment = subComment;
            Content = content;
            Created = DateTime.UtcNow;
        }
        
        public Guid Id { get; set; }
        
        [Required]
        public string? Content { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [Required]
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public Guid? CommentId { get; set; }
        public Comment? SubComment { get; set; }
        
    }
}
