namespace TodoAPI.Domain.Entities
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Rank { get; set; } = string.Empty;
    }
}