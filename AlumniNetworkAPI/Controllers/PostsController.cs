using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Posts;
using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using AlumniNetworkAPI.Services.PostServices;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostsController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetPosts()
        {
            string? keycloakId = this.User.GetId();

            if (keycloakId == null)
            {
                return BadRequest();
            }
            return _mapper.Map<List<PostReadDto>>(await _postService.GetPostsAsync(keycloakId));
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostReadDto>> GetPostById(int id)
        {
            try
            {
                Post post = await _postService.GetPostByIdAsync(id);
                var postDto = _mapper.Map<PostReadDto>(post);
                return Ok(postDto);
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostEditDto postDto)
        {
            try
            {
                await _postService.UpdatePostAsync(_mapper.Map<Post>(postDto));
                return NoContent();
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(PostCreateDto post)
        {
            string keycloakId = this.User.GetId();

            Post domainPost = _mapper.Map<Post>(post);
            domainPost.LastUpdated = DateTime.Now;
            try
            {
                domainPost = await _postService.AddPostAsync(domainPost, keycloakId);
                return CreatedAtAction("GetPosts",
                    new { id = domainPost.Id },
                    _mapper.Map<PostReadDto>(domainPost));
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid audience");
            }
            catch (Exception)
            {
                return Forbid();
            }
        }
    }
}
