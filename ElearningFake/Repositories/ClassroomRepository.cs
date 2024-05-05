using ElearningFake.Contracts;
using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace ElearningFake.Repositories
{
    public class ClassroomRepository : IClassroom
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
       
        
        public ClassroomRepository(AppDbContext appDbContext ,UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContext)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _httpContext = httpContext;
        } 
        public async Task<Classroom> AddClassroomAsync(ClassroomDTO model)
        {

            HttpContext httpcontext = _httpContext.HttpContext;
             

            var userId = httpcontext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Classroom classroom = new Classroom(model);
            classroom.CreatedAt = DateTime.Now;
            classroom.CreatedUser = userId;
            classroom.IsDeleted = false;
            _appDbContext.Add(classroom);
            await _appDbContext.SaveChangesAsync();


            // take  id classroom

            int classroomId = classroom.Id;
            ApplicationUser user = await _appDbContext.Users.FindAsync(userId);

            UserClassroom userClassroom = new UserClassroom
            {
                ClassroomId = classroomId,
                User = user,
                IsTeacher = true,
                IsExit = false,
                
            };
            _appDbContext.Add(userClassroom);
            await _appDbContext.SaveChangesAsync();


            return classroom;
        }

        public async Task DeleteClassroomAsync(int id)
        {
            Classroom foundClassroom = await _appDbContext.Classrooms.SingleOrDefaultAsync(x => x.Id == id);
            if (foundClassroom == null)
            {
                return ;
            }
            foundClassroom.IsDeleted = true;
            _appDbContext.Update(foundClassroom);
            await _appDbContext.SaveChangesAsync();

        }

        public async Task<bool> ExitCLassroomAsync(int id)
        {
            string userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserClassroom userClassroom = await _appDbContext.UserClassrooms.FirstOrDefaultAsync(x => x.ClassroomId == id );
            if (userClassroom != null) {
                userClassroom.IsExit= true;
                await _appDbContext.SaveChangesAsync();
                return true;
            
            }

            return false;
        }

        public async Task<List<ClassroomModel>> GetClassroomAsync()
        {
            List<ClassroomModel> classroomModels = await _appDbContext.Classrooms.Where(x => !x.IsDeleted).Select(x => new ClassroomModel(x)).ToListAsync();
            if (classroomModels == null || classroomModels.Count() == 0 )
            {
                return null;
            }
 

            return classroomModels;
        }

        public async Task<ClassroomModel> GetClassroomByIdAsync(int id)
        {
            Classroom foundClassroom = await _appDbContext.Classrooms.FirstOrDefaultAsync(x =>x.Id == id);
            if (foundClassroom == null || foundClassroom.IsDeleted)
            {
                return null;
            }
            return new ClassroomModel(foundClassroom);

            
        }

        public async Task<List<ApplicationUser>> GetUserInClassroomAsync(int id)
        {
            var userClassrooms = await _appDbContext.UserClassrooms
               .Include(c => c.Classroom)
               .Include(c => c.User)
               .Where(uc => uc.ClassroomId == id && uc.IsExit == false)
               .ToListAsync();

            // Lấy danh sách ID người dùng từ danh sách UserClassroom
            var users = userClassrooms.Select(uc => uc.User).ToList();

            // Lấy thông tin người dùng từ bảng ApplicationUser
           //List<ApplicationUser> users = await _appDbContext.Users.Where(x => x.Equals(id)).ToListAsync(); 

            return users;

        }

        public async Task<bool> JoinCLassroomAsync(int id)
        {
            string userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isMember = await _appDbContext.Classrooms.AnyAsync(x => x.Id == id && x.CreatedUser == userId);

            if (!isMember)
            {
                UserClassroom userClassroom = new UserClassroom
                {
                    ClassroomId = id,
                    User = await _appDbContext.Users.FindAsync(userId),
                    IsTeacher = false,
                    IsExit = false
                };
                _appDbContext.UserClassrooms.Add(userClassroom);

                await _appDbContext.SaveChangesAsync();
                return true;
            }




            return false;
        }

        public async Task UpdateClassroomAsync(ClassroomModel model)
        {

           Classroom foundClassroom = await _appDbContext.Classrooms.SingleOrDefaultAsync(x => x.Id== model.Id);
           if (foundClassroom == null || foundClassroom.IsDeleted) { return; }
            _appDbContext.Update(foundClassroom);
            await _appDbContext.SaveChangesAsync();
        }
        // add createfunction join classroom 
        // create function leave classroom(add actribute IsExit in table userClassroom )
        // get all user classroom 


    


    }
}
