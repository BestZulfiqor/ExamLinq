using Domain.Dtos.Projects;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;
[ApiController]
[Route("api/[controller]")]

public class ProjectController(IProjectService service)
{
    [HttpPost]
    public async Task<Response<GetProjectDto>> AddProject(CreateProjectDto projectDto){
        return await service.AddProject(projectDto);
    }
    [HttpPut("{id:int}")]
    public async Task<Response<GetProjectDto>> UpdateProject(int id, UpdateProjectDto projectDto){
        return await service.UpdateProject(id, projectDto);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteProject(int id){
        return await service.DeleteProject(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetProjectDto>> GetProject(int id){
        return await service.GetProject(id);
    }
    [HttpGet("get-project-with-most-task")]
    public async Task<Response<GetProjectDto>> GetProjectWithMostTasks(){
        return await service.GetProjectWithMostTasks();
    }
}
