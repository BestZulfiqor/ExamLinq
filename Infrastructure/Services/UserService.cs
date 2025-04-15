using System.Net;
using Domain.Dtos.Tasks;
using Domain.Dtos.Users;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<GetUserDto>> AddUser(CreateUserDto createUserDto)
    {
        var user = new User()
        {
            Name = createUserDto.Name,
            Email = createUserDto.Email,
            RegistrationDate = createUserDto.RegistrationDate,
        };

        await context.Users.AddAsync(user);
        var result = await context.SaveChangesAsync();

        var dto = new GetUserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
        };

        return result == 0
            ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not added!")
            : new Response<GetUserDto>(dto);
    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        var exist = await context.Users.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Users not found!");
        }

        context.Users.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("User not deleted")
            : new Response<string>("User deleted");
    }

    public async Task<Response<GetUserDto>> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.NotFound, "User not found");
        }

        var dto = new GetUserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
        };

        return new Response<GetUserDto>(dto);
    }

    public async Task<Response<GetUserDto>> GetUserWithMostTasks()
    {
        var max = await context.Users.MaxAsync(n => n.Tasks.Count);
        var user = await context.Users
            .FirstOrDefaultAsync(n => n.Tasks.Count == max);
        var dto = new GetUserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
        };

        return new Response<GetUserDto>(dto);
    }

    public async Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto createUserDto)
    {
        var exist = await context.Users.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.NotFound, "User not found");
        }
        exist.Name = createUserDto.Name;
        exist.Email = createUserDto.Email;
        exist.RegistrationDate = createUserDto.RegistrationDate;
        context.Users.Update(exist);

        var dto = new GetUserDto()
        {
            Id = exist.Id,
            Name = exist.Name,
            Email = exist.Email,
            RegistrationDate = exist.RegistrationDate,
        };
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not updated")
            : new Response<GetUserDto>(dto);
    }

}
