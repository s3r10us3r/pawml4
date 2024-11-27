namespace pawm.Models
{
    public class TodoElement : Entity
    {
        public string ListId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
