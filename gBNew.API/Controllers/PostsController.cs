using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gBNew.API.DTOs;
using gBNew.API.Roles;
using gBNew.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace gBNew.API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize]
public class PostsController : ControllerBase
{
  private readonly IPostService _postService;

  public PostsController(IPostService postService)
  {
    _postService = postService;
  }

  [HttpGet]
  public async Task<ActionResult<List<PostDTO>>> Get()
  {
    var postDto = await _postService.GetPosts();
    if (postDto == null)
    {
      return NotFound();
    }
    return Ok(postDto);
  }

  [HttpGet("{id:int}", Name = "GetPost")]
  public async Task<ActionResult<PostDTO>> GetById(int id)
  {
    var postDto = await _postService.GetPostById(id);
    if (postDto == null)
    {
      return NotFound();
    }
    return Ok(postDto);
  }

  [HttpPost]
  public async Task<ActionResult> Post([FromBody] PostDTO postDto)
  {
    if (postDto == null)
    {
      return BadRequest();
    }

    await _postService.AddPost(postDto);

    return new CreatedAtRouteResult("GetPost", new { id = postDto.PostId }, postDto);

  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> Put(int id, [FromBody] PostDTO postDto)
  {
    if (id != postDto.PostId)
    {
      return BadRequest("Invalid Data");
    }
    if (postDto == null)
    {
      return BadRequest();
    }

    await _postService.UpdatePost(postDto);
    return Ok(postDto);
  }

  [HttpDelete("{id:int}")]
  // [Authorize(Roles = Role.Admin)]
  public async Task<ActionResult> Delete(int id)
  {
    var postDto = await _postService.GetPostById(id);
    if (postDto == null)
    {
      return NotFound("User not found");
    }
    await _postService.DeletePost(id);
    return Ok(postDto);
  }
}

