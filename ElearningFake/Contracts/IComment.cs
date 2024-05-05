using ElearningFake.Data;
using ElearningFake.DTOs;

namespace ElearningFake.Contracts
{
    public interface IComment
    {
        public Task<Comment> PostAsync(CommentDTO comment,int id);
        public Task<List<Comment>> GetAsync(int id);

    }
}
