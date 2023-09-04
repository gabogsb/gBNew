using AutoMapper;
using gBNew.API.Models;

namespace gBNew.API.DTOs.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<User, UserDTO>().ReverseMap();
    CreateMap<Post, PostDTO>().ReverseMap();
  }
}
