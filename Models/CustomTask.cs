namespace KorpAPI.Models
{
    public class CustomTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public CustomTask(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}