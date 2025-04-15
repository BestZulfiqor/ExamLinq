using Domain.Dtos.Tasks;
using Domain.Entities;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ITaskService
{
    Task<Response<GetTaskDto>> AddTask(CreateTaskDto task);
    Task<Response<GetTaskDto>> UpdateTask(int id, UpdateTaskDto task);
    Task<Response<string>> DeleteTask(int id);
    Task<Response<GetTaskDto>> GetTask(int id);
    Task<Response<List<GetTaskDto>>> GetTaskByProject(string project);
    Task<Response<List<GetTaskDto>>> GetTaskByUser(string user);
    Task<Response<List<GetTaskDto>>> GetTaskDueSoon(int n);
}
