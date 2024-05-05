using ElearningFake.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ElearningFake.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Boolean IsDelete { get; set; }

        // Relationship 1-1
        public ApplicationUser ApplicationUser { get; set; }

        // n - 1
        public int PostId { get; set; }
        public Post Post { get; set; }


        public Comment() { }
        public Comment(CommentDTO model)
        {
            Description = model.Description!;
        }
    }
}
