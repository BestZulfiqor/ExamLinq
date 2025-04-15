using Domain.Dtos.TaskAssigments;
using Domain.Entities;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ITaskAssignmentService
{
    Task<Response<GetTaskAssignmentDto>> AssignTask(TaskAssigment taskAssigment);
    Task<Response<GetTaskAssignmentDto>> UpdateAssign(int id, TaskAssigment taskAssigment);
    Task<Response<string>> DeleteAssign(int id);
    Task<Response<GetTaskAssignmentDto>> GetAssignment(int id);
    Task<Response<List<GetTaskAssignmentDto>>> GetAssignmentsByUser(string name);
    Task<Response<List<GetTaskAssignmentDto>>> GetAssignmentsByTask(string task);

}
