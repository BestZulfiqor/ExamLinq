using Domain.Dtos.Projects;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IProjectService
{
    Task<Response<GetProjectDto>> AddProject(CreateProjectDto projectDto);
    Task<Response<GetProjectDto>> UpdateProject(int id, UpdateProjectDto projectDto);
    Task<Response<string>> DeleteProject(int id);
    Task<Response<GetProjectDto>> GetProject(int id);
    Task<Response<GetProjectDto>> GetProjectWithMostTasks();
}
