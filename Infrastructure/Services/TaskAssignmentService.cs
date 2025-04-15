using System.Net;
using Domain.Dtos.TaskAssigments;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TaskAssignmentService(DataContext context) : ITaskAssignmentService
{
    public async Task<Response<GetTaskAssignmentDto>> AssignTask(TaskAssigment taskAssigment)
    {
        var task = new TaskAssigment()
        {
            AssignedDate = taskAssigment.AssignedDate,
            TaskId = taskAssigment.TaskId,
            UserId = taskAssigment.UserId
        };

        await context.TaskAssigments.AddAsync(task);
        var result = await context.SaveChangesAsync();

        var dto = new GetTaskAssignmentDto()
        {
            Id = task.Id,
            AssignedDate = taskAssigment.AssignedDate,
            TaskId = taskAssigment.TaskId,
            UserId = taskAssigment.UserId
        };

        return result == 0
            ? new Response<GetTaskAssignmentDto>(HttpStatusCode.BadRequest, "TaskAssignment not added!")
            : new Response<GetTaskAssignmentDto>(dto);
    }

    public async Task<Response<string>> DeleteAssign(int id)
    {
        var exist = await context.TaskAssigments.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("TaskAssigments not found!");
        }

        context.TaskAssigments.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("TaskAssigments not deleted")
            : new Response<string>("TaskAssigments deleted");
    }

    public async Task<Response<GetTaskAssignmentDto>> GetAssignment(int id)
    {
        var taskAssigment = await context.TaskAssigments.FindAsync(id);
        if (taskAssigment == null)
        {
            return new Response<GetTaskAssignmentDto>(HttpStatusCode.NotFound, "TaskAssigment not found");
        }

        var dto = new GetTaskAssignmentDto()
        {
            Id = taskAssigment.Id,
            AssignedDate = taskAssigment.AssignedDate,
            TaskId = taskAssigment.TaskId,
            UserId = taskAssigment.UserId
        };

        return new Response<GetTaskAssignmentDto>(dto);
    }

    public async Task<Response<List<GetTaskAssignmentDto>>> GetAssignmentsByTask(string task)
    {
        var AssignTask = await context.TaskAssigments
            .Where(n => n.Task.Title == task)
            .Select(n => new GetTaskAssignmentDto
            {
                Id = n.Id,
                AssignedDate = n.AssignedDate,
                TaskId = n.TaskId,
                UserId = n.UserId
            }).ToListAsync();
        return new Response<List<GetTaskAssignmentDto>>(AssignTask);
    }

    public async Task<Response<List<GetTaskAssignmentDto>>> GetAssignmentsByUser(string name)
    {
        var AssignTask = await context.TaskAssigments
            .Where(n => n.Task.User.Name == name)
            .Select(n => new GetTaskAssignmentDto
            {
                Id = n.Id,
                AssignedDate = n.AssignedDate,
                TaskId = n.TaskId,
                UserId = n.UserId
            }).ToListAsync();
        return new Response<List<GetTaskAssignmentDto>>(AssignTask);
    }

    public async Task<Response<GetTaskAssignmentDto>> UpdateAssign(int id, TaskAssigment taskAssigment)
    {
        var exist = await context.TaskAssigments.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetTaskAssignmentDto>(HttpStatusCode.NotFound, "TaskAssigments not found");
        }
        exist.AssignedDate = taskAssigment.AssignedDate;
        exist.TaskId = taskAssigment.TaskId;
        exist.UserId = taskAssigment.UserId;
        context.TaskAssigments.Update(exist);

        var dto = new GetTaskAssignmentDto()
        {
            Id = exist.Id,
            AssignedDate = taskAssigment.AssignedDate,
            TaskId = taskAssigment.TaskId,
            UserId = taskAssigment.UserId
        };
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<GetTaskAssignmentDto>(HttpStatusCode.BadRequest, "TaskAssigments not updated")
            : new Response<GetTaskAssignmentDto>(dto);
    }

}
