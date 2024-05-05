using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Model;

namespace ElearningFake.Contracts
{
    public interface IClassroom
    {
        public Task<List<ClassroomModel>> GetClassroomAsync();
        public Task<ClassroomModel> GetClassroomByIdAsync(int id);

        public Task<Classroom> AddClassroomAsync(ClassroomDTO model);

        public Task DeleteClassroomAsync(int id);
        public Task UpdateClassroomAsync(ClassroomModel model);

        public Task <bool> JoinCLassroomAsync(int id);

        public Task<bool> ExitCLassroomAsync(int id);

        public Task<List<ApplicationUser>> GetUserInClassroomAsync(int id);
    }
}
