using AutoMapper;
using ElearningFake.Contracts;
using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Models;
using ElearningFake.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml.Linq;

namespace ElearningFake.Repositories
{
    public class CommnetRepository : IComment
    {
        private readonly AppDbContext _AppDbContext;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IMapper _Mapper;

        public CommnetRepository(AppDbContext appDbContext,IHttpContextAccessor httpContextAccessor,IMapper mapper)
        {
            _AppDbContext = appDbContext;
            _HttpContextAccessor = httpContextAccessor;
            _Mapper = mapper;
        }

        public async Task<List<CommentViewModel>> GetAsync(int id)
        {
            
            var comment = await _AppDbContext.Comments.Where(x => x.PostId == id && !x.IsDelete).Include(x => x.ApplicationUser).ToListAsync();
            var commentViewModel = comment.Select(comment => new CommentViewModel
            {
                Description = comment.Description,
                CreatedAt = comment.CreatedAt,
                UserName = comment.ApplicationUser != null ? comment.ApplicationUser.UserName : "Unknown"
            }).ToList();
            //return _Mapper.Map<List<CommentModel>>(comment);
            return commentViewModel;

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
