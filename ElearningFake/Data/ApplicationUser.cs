using Microsoft.AspNetCore.Identity;

namespace ElearningFake.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; } 

        // Relationship 1-n userClassroom
        public ICollection<UserClassroom> UserClassrooms { get; set; }

        // Relationship 1-n Post
        public ICollection<Post> Posts { get; set; }


    }
}
