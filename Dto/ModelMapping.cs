using ProjectBlu.Dto.Authentication;
using ProjectBlu.Models;

namespace ProjectBlu.Dto;

public class ModelMapping : Profile
{
    public ModelMapping()
    {
        CreateMap<User, UserResponse>();
    }
}
