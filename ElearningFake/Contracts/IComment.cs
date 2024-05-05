using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Models;
using ElearningFake.ViewModel;

namespace ElearningFake.Contracts
{
    public interface IComment
    {
        public Task<Comment> PostAsync(CommentDTO comment,int id);
        public Task<List<CommentViewModel>> GetAsync(int id);

    }
}
