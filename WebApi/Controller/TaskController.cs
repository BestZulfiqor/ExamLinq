using Domain.Dtos.Tasks;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService service)
{
    [HttpPost]
    public async Task<Response<GetTaskDto>> AddTask(CreateTaskDto task)
    {
        return await service.AddTask(task);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetTaskDto>> UpdateTask(int id, UpdateTaskDto task)
    {
        return await service.UpdateTask(id, task);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteTask(int id)
    {
        return await service.DeleteTask(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetTaskDto>> GetTask(int id)
    {
        return await service.GetTask(id);
    }
    [HttpGet("project/{project}")]
    public async Task<Response<List<GetTaskDto>>> GetTaskByProject(string project)
    {
        return await service.GetTaskByProject(project);
    }
    [HttpGet("task-by-user/{user}")]
    public async Task<Response<List<GetTaskDto>>> GetTaskByUser(string user)
    {
        return await service.GetTaskByUser(user);
    }
    [HttpGet("days/{n}")]
    public async Task<Response<List<GetTaskDto>>> GetTaskDueSoon(int n)
    {
        return await service.GetTaskDueSoon(n);
    }
}
