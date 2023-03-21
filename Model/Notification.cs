using System.ComponentModel.DataAnnotations;


namespace BachelorOppgaveBackend.Model;

public class Notification
{
    public Notification(){}

    public Notification(string type, User user)
    {
        Type = type;
        User = user;
        Created = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Created { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
}