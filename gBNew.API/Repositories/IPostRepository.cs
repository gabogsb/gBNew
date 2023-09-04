using gBNew.API.Models;

namespace gBNew.API.Repositories;

public interface IPostRepository
{
  Task<List<Post>> GetAllPosts();
  Task<Post> GetPostById(int id);
  Task<Post> Create(Post post);
  Task<Post> Update(Post post);
  Task<Post> Delete(int id);
}
