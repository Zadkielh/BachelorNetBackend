using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BachelorOppgaveBackend.Model
{
    public class Status
    {
        public Status()
        {
            Created = DateTime.UtcNow;
        }

        public Status(Guid user, string type, string description)
        {
            UserId = user;
            Type = type;
            Description = description;
            Created = DateTime.UtcNow;
        }
        
        public Guid Id { get; set; }
        
        [Required]
        public string? Type { get; set; }
    
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }
        
        public Guid? UserId { get; set; }
    }
}
