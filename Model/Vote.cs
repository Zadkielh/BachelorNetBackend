namespace BachelorOppgaveBackend.Model
{
    public class Vote
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public DateOnly CreatedAt { get; set; }
    }
}
