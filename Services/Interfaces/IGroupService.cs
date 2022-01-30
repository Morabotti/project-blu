namespace ProjectBlu.Services.Interfaces;

public interface IGroupService
{
    Task<Response<GroupResponse>> CreateAsync(CreateGroupRequest request);
    Task<Response<GroupResponse>> GetByIdAsync(int id);
    Task<Response<List<GroupResponse>>> GetManyAsync();
    Task<Response<GroupResponse>> UpdateAsync(GroupResponse request);
    Task<Response<GroupResponse>> DeleteAsync(int id);
}
