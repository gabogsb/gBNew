using gBNew.API.DTOs;

namespace gBNew.API.Services;

public interface IPostService
{
  Task<List<PostDTO>> GetPosts();
  Task<PostDTO> GetPostById(int id);
  Task AddPost(PostDTO postDto);
  Task UpdatePost(PostDTO postDto);
  Task DeletePost(int id);
}
