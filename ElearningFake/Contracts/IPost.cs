using ElearningFake.Data;
using ElearningFake.DTOs;
using ElearningFake.Models;

namespace ElearningFake.Contracts
{
    public interface IPost
    {
        public Task<Post> AddPostAsync(PostDTO content,int id);

        public Task<List<PostModel>> GetPostAsync();

        public Task DeletePostAsync(int id);

        public Task UpdatePostAsync(PostModel content);

        

    }
}
