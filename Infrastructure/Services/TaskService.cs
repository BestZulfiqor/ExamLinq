using System.Net;
using Domain.Dtos.Tasks;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TaskService(DataContext context) : ITaskService
{
    public async Task<Response<GetTaskDto>> AddTask(CreateTaskDto task)
    {
        var task1 = new Domain.Entities.Task()
        {
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            UserId = task.UserId,
        };

        await context.Tasks.AddAsync(task1);
        var result = await context.SaveChangesAsync();

        var dto = new GetTaskDto()
        {
            Id = task1.Id,
            Title = task1.Title,
            Description = task1.Description,
            DueDate = task1.DueDate,
            ProjectId = task1.ProjectId,
            UserId = task1.UserId,
        };

        return result == 0
            ? new Response<GetTaskDto>(HttpStatusCode.BadRequest, "Task not added!")
            : new Response<GetTaskDto>(dto);
    }

    public async Task<Response<string>> DeleteTask(int id)
    {
        var exist = await context.Tasks.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Task not found!");
        }

        context.Tasks.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Task not deleted")
            : new Response<string>("Task deleted");
    }

    public async Task<Response<GetTaskDto>> GetTask(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task == null)
        {
            return new Response<GetTaskDto>(HttpStatusCode.NotFound, "Task not found");
        }

        var dto = new GetTaskDto()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            UserId = task.UserId,
        };

        return new Response<GetTaskDto>(dto);
    }

    public async Task<Response<List<GetTaskDto>>> GetTaskByProject(string project)
    {
        var task = await context.Tasks
            .Where(n => n.Project.Name == project)
            .Select(n => new GetTaskDto()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                DueDate = n.DueDate,
                ProjectId = n.ProjectId,
                UserId = n.UserId,
            })
            .ToListAsync();

        return new Response<List<GetTaskDto>>(task);
    }

    public async Task<Response<List<GetTaskDto>>> GetTaskByUser(string user)
    {
        var task = await context.Tasks
            .Where(n => n.User.Name == user)
            .Select(n => new GetTaskDto()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                DueDate = n.DueDate,
                ProjectId = n.ProjectId,
                UserId = n.UserId,
            })
            .ToListAsync();

        return new Response<List<GetTaskDto>>(task);
    }

    public async Task<Response<List<GetTaskDto>>> GetTaskDueSoon(int n)
    {
        var date = DateTime.Now.AddDays(n);
        var task = await context.Tasks
            .Where(n => date.Day >= n.DueDate.Day)
            .Select(n => new GetTaskDto()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                DueDate = n.DueDate,
                ProjectId = n.ProjectId,
                UserId = n.UserId,
            })
            .ToListAsync();

        return new Response<List<GetTaskDto>>(task);
    }

    public async Task<Response<GetTaskDto>> UpdateTask(int id, UpdateTaskDto task)
    {
        var exist = await context.Tasks.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetTaskDto>(HttpStatusCode.NotFound, "Task not found");
        }
        exist.Description = task.Description;
        exist.DueDate = task.DueDate;
        exist.ProjectId = task.ProjectId;
        exist.Title = task.Title;
        exist.UserId = task.UserId;
        context.Tasks.Update(exist);

        var dto = new GetTaskDto()
        {
            Id = exist.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            UserId = task.UserId,
        };
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<GetTaskDto>(HttpStatusCode.BadRequest, "Task not updated")
            : new Response<GetTaskDto>(dto);
    }

}
