using gBNew.API.Context;
using gBNew.API.Models;
using Microsoft.EntityFrameworkCore;

namespace gBNew.API.Repositories;

public class PostRepository : IPostRepository
{

  private readonly AppDbContext _context;

  public PostRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<Post>> GetAllPosts()
  {
    return await _context.Posts.ToListAsync();
  }
  public async Task<Post> GetPostById(int id)
  {
    return await _context.Posts.Where(u => u.PostId == id).FirstOrDefaultAsync();
  }
  public async Task<Post> Create(Post post)
  {
    _context.Posts.Add(post);
    await _context.SaveChangesAsync();
    return post;
  }
  public async Task<Post> Update(Post post)
  {
    _context.Entry(post).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return post;
  }
  public async Task<Post> Delete(int id)
  {
    var post = await GetPostById(id);
    _context.Posts.Remove(post);
    await _context.SaveChangesAsync();
    return post;
  }
}
