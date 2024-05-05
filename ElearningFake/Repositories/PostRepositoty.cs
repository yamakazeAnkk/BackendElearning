using ElearningFake.Contracts;
using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Model;
using ElearningFake.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElearningFake.Repositories
{
    public class PostRepositoty : IPost
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        public PostRepositoty(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _httpContext = httpContext;
        }
        public async Task<Post> AddPostAsync(PostDTO content, int id)
        {
            HttpContext httpcontext = _httpContext.HttpContext;

            var userId = httpcontext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = await _appDbContext.Users.Include(x => x.UserClassrooms).FirstOrDefaultAsync(x => x.Id == userId);


            bool isTeacher = user.UserClassrooms.Any(x => x.IsTeacher == true);
            if (isTeacher)
            {
                Post post = new Post(content);
                post.CreatedAt = DateTime.Now;
                post.IsDelete = false;
                post.ApplicationUser = user;
                post.ClassroomId = id;


                _appDbContext.Update(post);
                await _appDbContext.SaveChangesAsync();

                return post;
            }

            throw new Exception("User is not a teacher in any classroom.");


        }

        public async Task DeletePostAsync(int id)
        {
            Post post = await _appDbContext.Posts.SingleOrDefaultAsync(p => p.Id == id);
            if (post != null) { return; }

            post.IsDelete = true;
            _appDbContext.Update(post);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<PostModel>> GetPostAsync()
        {
            List<PostModel> postModels = await _appDbContext.Posts.Where(x => x.IsDelete == false).Select(x => new PostModel{


                Description = x.Description,CreatedAt = x.CreatedAt,ApplicationUser = x.ApplicationUser
            }).ToListAsync();
            if (postModels == null || postModels.Count() == 0)
            {
                return null;
            }


            return postModels;
        }

        public async Task UpdatePostAsync(PostModel content)
        {
            Post post = await _appDbContext.Posts.SingleOrDefaultAsync(x  => x.Id == content.Id);
            if (post != null || post.IsDelete == true ) {  }
            _appDbContext.Update(post);
            await _appDbContext.SaveChangesAsync();

            
        }
    }
}
