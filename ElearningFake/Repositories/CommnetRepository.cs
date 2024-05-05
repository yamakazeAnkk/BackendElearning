using ElearningFake.Contracts;
using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElearningFake.Repositories
{
    public class CommnetRepository : IComment
    {
        private readonly AppDbContext _AppDbContext;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public CommnetRepository(AppDbContext appDbContext,IHttpContextAccessor httpContextAccessor)
        {
            _AppDbContext = appDbContext;
            _HttpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Comment>> GetAsync(int id)
        {
            List<Comment> comments = await _AppDbContext.Comments.Where(x => x.PostId == id && !x.IsDelete).Select(x=> new Comment { ApplicationUser =x.ApplicationUser}).ToListAsync();
            {
                return comments;
            }
            return comments;
        }

        public async Task<Comment> PostAsync(CommentDTO comment, int id)
        {
            HttpContext httpContext = _HttpContextAccessor.HttpContext;
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser user = await _AppDbContext.Users.FindAsync(userId);
            
            Comment commnetInStudent = new Comment(comment);
            commnetInStudent.CreatedAt = DateTime.UtcNow;
            commnetInStudent.IsDelete = false;
            commnetInStudent.ApplicationUser = user;
            commnetInStudent.PostId = id;

            _AppDbContext.Update(commnetInStudent);
            await _AppDbContext.SaveChangesAsync();
            

            return commnetInStudent;
        }
    }
}
