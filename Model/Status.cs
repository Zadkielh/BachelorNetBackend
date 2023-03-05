namespace BachelorOppgaveBackend.Model
{
    public class Status
    {
        public Guid id { get; set; }
        public int post_id { get; set; }
        public int user_id { get; set; }
        public string? status { get; set; }

        public string? description { get; set; }

        public DateTime created_at { get; set; }
    }
}
