using ProjectBlu.Models;

namespace ProjectBlu.Services.Interfaces;

public interface IProjectService
{
    Task<Response<ProjectResponse>> CreateAsync(CreateProjectRequest request);
    Task<Response<ProjectResponse>> GetByIdAsync(int id);
    Task<Response<List<ProjectResponse>>> GetManyAsync();
    Task<Response<ProjectResponse>> UpdateAsync(ProjectResponse request);
    Task<Response<ProjectResponse>> DeleteAsync(int id);
}
