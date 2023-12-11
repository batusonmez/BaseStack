namespace Todo.Domain.Entities
{
    public class Todo
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Todo()
        {
                Title = string.Empty;
        }
    }
}
