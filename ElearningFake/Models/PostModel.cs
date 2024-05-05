using ElearningFake.Data;
using ElearningFake.DTOs;

namespace ElearningFake.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Boolean IsDelete { get; set; }

        // Relationship 1-1
        public ApplicationUser ApplicationUser { get; set; }

        // n - 1
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

        // Relationship 1-n with Comment
        public ICollection<Comment> Comments { get; set; }

        public PostModel() { }
        public PostModel(PostDTO model)
        {
            Description = model.Description!;
            
        }
    }
}
