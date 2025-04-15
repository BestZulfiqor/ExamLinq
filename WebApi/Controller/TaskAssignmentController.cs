using Domain.Dtos.TaskAssigments;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]

public class TaskAssignmentController(ITaskAssignmentService service)
{
    [HttpPost]
    public async Task<Response<GetTaskAssignmentDto>> AssignTask(TaskAssigment taskAssigment){
        return await service.AssignTask(taskAssigment);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetTaskAssignmentDto>> UpdateAssign(int id, TaskAssigment taskAssigment){
        return await service.UpdateAssign(id, taskAssigment);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAssign(int id){
        return await service.DeleteAssign(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetTaskAssignmentDto>> GetAssignment(int id){
        return await service.GetAssignment(id);
    }
    [HttpGet("name/{name}")]
    public async Task<Response<List<GetTaskAssignmentDto>>> GetAssignmentsByUser(string name){
        return await service.GetAssignmentsByUser(name);
    }
    [HttpGet("task/{task}")]
    public async Task<Response<List<GetTaskAssignmentDto>>> GetAssignmentsByTask(string task){
        return await service.GetAssignmentsByTask(task);
    }
}
