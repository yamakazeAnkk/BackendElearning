using ElearningFake.Data;
using ElearningFake.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ElearningFake.Models
{
    public class CommentModel
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


        public CommentModel() { }
        public CommentModel(Comment model)
        {
            Id = model.Id;
            Description = model.Description!;
            CreatedAt = model.CreatedAt;
            ApplicationUser = model.ApplicationUser;
            PostId = model.PostId;


        }
    }
}
