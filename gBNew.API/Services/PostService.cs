using AutoMapper;
using gBNew.API.DTOs;
using gBNew.API.Models;
using gBNew.API.Repositories;

namespace gBNew.API.Services;

public class PostService : IPostService
{

  private readonly IPostRepository _postRepository;
  private readonly IMapper _mapper;

  public PostService(IPostRepository postRepository, IMapper mapper)
  {
    _postRepository = postRepository;
    _mapper = mapper;
  }

  public async Task<List<PostDTO>> GetPosts()
  {
    var postEntity = await _postRepository.GetAllPosts();
    return _mapper.Map<List<PostDTO>>(postEntity);
  }

  public async Task<PostDTO> GetPostById(int id)
  {
    var postEntity = await _postRepository.GetPostById(id);
    return _mapper.Map<PostDTO>(postEntity);
  }

  public async Task AddPost(PostDTO postDto)
  {
    var postEntity = _mapper.Map<Post>(postDto);
    await _postRepository.Create(postEntity);
    postDto.PostId = postEntity.PostId;
  }

  public async Task UpdatePost(PostDTO postDto)
  {
    var postEntity = _mapper.Map<Post>(postDto);
    await _postRepository.Update(postEntity);
  }

  public async Task DeletePost(int id)
  {
    var postEntity = _postRepository.GetPostById(id).Result;
    await _postRepository.Delete(postEntity.PostId);
  }
}





