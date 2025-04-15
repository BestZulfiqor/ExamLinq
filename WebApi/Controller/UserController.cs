using Domain.Dtos.Users;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service)
{
    [HttpPost]
    public async Task<Response<GetUserDto>> AddUser(CreateUserDto createUserDto){
        return await service.AddUser(createUserDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto createUserDto){
        return await service.UpdateUser(id, createUserDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteUser(int id){
        return await service.DeleteUser(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetUserDto>> GetUser(int id){
        return await service.GetUser(id);
    }
    [HttpGet("user-with-most-tasks")]
    public async Task<Response<GetUserDto>> GetUserWithMostTasks(){
        return await service.GetUserWithMostTasks();
    }
}
