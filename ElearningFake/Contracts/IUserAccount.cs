using ElearningFake.DTOs;
using static ElearningFake.Response.ServiceResponses;

namespace ElearningFake.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
