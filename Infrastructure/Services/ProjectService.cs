using System.Net;
using Domain.Dtos.Projects;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProjectService(DataContext context) : IProjectService
{
    public async Task<Response<GetProjectDto>> AddProject(CreateProjectDto projectDto)
    {
        var project = new Project()
        {
            Name = projectDto.Name,
            Description = projectDto.Description,
            StartDate = projectDto.StartDate,
            EndDate = projectDto.EndDate
        };

        await context.Projects.AddAsync(project);
        var result = await context.SaveChangesAsync();

        var dto = new GetProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };

        return result == 0
            ? new Response<GetProjectDto>(HttpStatusCode.BadRequest, "Project not added!")
            : new Response<GetProjectDto>(dto);
    }

    public async Task<Response<string>> DeleteProject(int id)
    {
        var exist = await context.Projects.FindAsync(id);
        if (exist == null)
        {
            return new Response<string>("Project not found!");
        }

        context.Projects.Remove(exist);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<string>("Project not deleted")
            : new Response<string>("Project deleted");
    }

    public async Task<Response<GetProjectDto>> GetProject(int id)
    {
        var project = await context.Projects.FindAsync(id);
        if (project == null)
        {
            return new Response<GetProjectDto>(HttpStatusCode.NotFound, "Project not found");
        }

        var dto = new GetProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };

        return new Response<GetProjectDto>(dto);
    }

    public async Task<Response<GetProjectDto>> GetProjectWithMostTasks()
    {
        var max = await context.Projects.MaxAsync(n => n.Tasks.Count);
        var project = await context.Projects
            .FirstOrDefaultAsync(n => n.Tasks.Count == max);
        var dto = new GetProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };

        return new Response<GetProjectDto>(dto);
    }

    public async Task<Response<GetProjectDto>> UpdateProject(int id, UpdateProjectDto projectDto)
    {
        var exist = await context.Projects.FindAsync(id);
        if (exist == null)
        {
            return new Response<GetProjectDto>(HttpStatusCode.NotFound, "Project not found");
        }
        exist.Description = projectDto.Description;
        exist.StartDate = projectDto.StartDate;
        exist.EndDate = projectDto.EndDate;
        exist.Name = projectDto.Name;
        exist.Description = projectDto.Description;
        exist.Description = projectDto.Description;
        context.Projects.Update(exist);

        var dto = new GetProjectDto()
        {
            Id = exist.Id,
            Name = exist.Name,
            Description = exist.Description,
            StartDate = exist.StartDate,
            EndDate = exist.EndDate
        };
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new Response<GetProjectDto>(HttpStatusCode.BadRequest, "Project not updated")
            : new Response<GetProjectDto>(dto);
    }

}
