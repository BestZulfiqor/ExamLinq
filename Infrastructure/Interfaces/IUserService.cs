using Domain.Dtos.Users;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<GetUserDto>> AddUser(CreateUserDto createUserDto);
    Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto createUserDto);
    Task<Response<string>> DeleteUser(int id);
    Task<Response<GetUserDto>> GetUser(int id);
    Task<Response<GetUserDto>> GetUserWithMostTasks();
}
