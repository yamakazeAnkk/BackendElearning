using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElearningFake.Data
{
    public class UserClassroom
    {
        [Key]
        public int Id { get; set; }
        public Boolean? IsTeacher { get; set; }
        public Boolean? IsExit { get; set; }


        public int ClassroomId { get; set; }
        [JsonIgnore]
        public Classroom Classroom { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
} 
