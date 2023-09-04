using gBNew.API.Models;

namespace gBNew.API.Repositories;

public interface IUserRepository
{
  Task<List<User>> GetAllUsers();
  Task<User> GetUserById(int id);
  Task<User> Create(User user);
  Task<User> Update(User user);
  Task<User> Delete(int id);
}
