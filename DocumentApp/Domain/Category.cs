namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}