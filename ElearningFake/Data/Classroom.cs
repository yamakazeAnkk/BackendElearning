using ElearningFake.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ElearningFake.Data
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public string? Type { get; set; } = null!;
        public string? CreatedUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Boolean IsDeleted { get; set; }

        // Relationship 1-n
        public ICollection<UserClassroom> UserClassrooms { get; set; } = [];

        // Relationship 1-n
        public ICollection<Post> Posts { get; set; }

        public Classroom() { }
        public Classroom(ClassroomDTO model)
        {
            Name = model.Name;
            Description = model.Description;
            Type = model.Type;
        }

    }
}
