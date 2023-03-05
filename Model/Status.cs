namespace BachelorOppgaveBackend.Model
{
    public class Status
    {
        public Guid id { get; set; }
        public Guid post_id { get; set; }
        public Guid user_id { get; set; }
        public string? status { get; set; }

        public string? description { get; set; }

        public DateTime created_at { get; set; }
    }
}
