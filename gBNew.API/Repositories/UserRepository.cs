using gBNew.API.Context;
using gBNew.API.Models;
using Microsoft.EntityFrameworkCore;

namespace gBNew.API.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext _context;
  public UserRepository(AppDbContext context)
  {
    _context = context;
  }
  public async Task<List<User>> GetAllUsers()
  {
    return await _context.Users.ToListAsync();
  }
  public async Task<User> GetUserById(int id)
  {
    return await _context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();
  }
  public async Task<User> Create(User user)
  {
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task<User> Update(User user)
  {
    _context.Entry(user).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return user;
  }
  public async Task<User> Delete(int id)
  {
    var user = await GetUserById(id);
    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
    return user;
  }
}
