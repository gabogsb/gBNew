using gBNew.API.DTOs;

namespace gBNew.API.Services;

public interface IUserService
{
  Task<List<UserDTO>> GetAllUsers();
  Task<UserDTO> GetUserById(int id);
  Task CreateUser(UserDTO userDto);
  Task UpdateUser(UserDTO userDto);
  Task DeleteUser(int id);
}
