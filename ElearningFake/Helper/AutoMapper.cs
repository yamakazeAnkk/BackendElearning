using AutoMapper;
using ElearningFake.Data;
using ElearningFake.Models;
using ElearningFake.ViewModel;

namespace ElearningFake.Helper
{
    public class AutoMapper: Profile
    {
        public AutoMapper() {
            CreateMap<Comment, CommentModel>().ReverseMap();
        }
    }
}
