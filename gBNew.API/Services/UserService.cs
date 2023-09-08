using AutoMapper;
using gBNew.API.DTOs;
using gBNew.API.Models;
using gBNew.API.Repositories;

namespace gBNew.API.Services;

public class UserService : IUserService
{

  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public UserService(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  public async Task<List<UserDTO>> GetAllUsers()
  {
    var userEntity = await _userRepository.GetAllUsers();
    return _mapper.Map<List<UserDTO>>(userEntity);
  }
  public async Task<UserDTO> GetUserById(int id)
  {
    var userEntity = await _userRepository.GetUserById(id);
    return _mapper.Map<UserDTO>(userEntity);
  }
  public async Task CreateUser(UserDTO userDto)
  {
    var userEntity = _mapper.Map<User>(userDto);
    await _userRepository.Create(userEntity);
    userDto.UserId = userEntity.UserId;
  }
  public async Task UpdateUser(UserDTO userDto)
  {
    var userEntity = _mapper.Map<User>(userDto);
    await _userRepository.Update(userEntity);

  }
  public async Task DeleteUser(int id)
  {
    var userEntity = _userRepository.GetUserById(id).Result;
    await _userRepository.Delete(userEntity.UserId);
  }

}
