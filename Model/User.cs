using System.ComponentModel.DataAnnotations;

namespace BachelorOppgaveBackend.Model
{
    public class User
    {
        public User() {}

        public User(Guid userId, string userName, string email, Guid userRoleId)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Created = DateTime.UtcNow;
            UserRoleId = userRoleId;

        }
            
        public Guid Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public string? UserName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        public DateTime Created { get; set; }

        [Required]
        public Guid UserRoleId { get; set; }    
        public UserRole UserRole { get; set; }
        
    }
}

