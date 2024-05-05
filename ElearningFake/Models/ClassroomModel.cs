using ElearningFake.Data;
using System.ComponentModel.DataAnnotations;

namespace ElearningFake.Model
{
    public class ClassroomModel
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public string? Type { get; set; } = null!;

        public String? CreatedUser { get; set; }
        public DateTime CreatedAt { get; set; }

        public Boolean? IsDeleted { get; set; }

        public ClassroomModel() { }
        public ClassroomModel(Classroom classroom)
        {
            Id = classroom.Id;
            Name = classroom.Name;
            Description = classroom.Description;
            Type = classroom.Type;
            CreatedUser = classroom.CreatedUser;
            CreatedAt = classroom.CreatedAt;
            IsDeleted = classroom.IsDeleted;

        }
    }
}
