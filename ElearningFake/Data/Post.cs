using ElearningFake.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElearningFake.Data
{
    public class Post
    {
        [Key]
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

        public Post() {}
        public Post(PostDTO model)
        {
            Description = model.Description;
           
            
        }
    }
}
