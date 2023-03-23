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
using Newtonsoft.Json;
using System.Collections.Generic;
using Azure;

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
        #region CRUD
        #region READ
        /// <summary>
        ///     Returns a list of posts.
        /// </summary>
        /// <remarks>
        ///     Optionally accepts query parameters: search, filter, limit and offset.
        /// </remarks>
        /// <param name="search">Search posts by name.</param>
        /// <param name="filter">Sort posts by filters: descending or ascending.</param>
        /// <param name="limit">The maximum number of posts to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of posts within the overall list of posts, effectively skipping a certain number of posts.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetPosts([FromQuery] string? search = null, [FromQuery] string? filter = null,[FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            string? keycloakId = this.User.GetId();

            if (keycloakId == null)
            {
                return BadRequest();
            }
            return _mapper.Map<List<PostReadDto>>(await _postService.GetPostsAsync(keycloakId, search, filter, limit, offset));
        }

        /// <summary>
        ///     Returns a list of posts that were sent as direct messages to the requesting user.
        /// </summary>
        /// <remarks>
        ///     Optionally accepts query parameters: search, filter, limit and offset.
        /// </remarks>
        /// <param name="search">Search posts by name.</param>
        /// <param name="filter">Sort posts by filters: descending or ascending.</param>
        /// <param name="limit">The maximum number of posts to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of posts within the overall list of posts, effectively skipping a certain number of posts.</param>
        [HttpGet("messages/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetRecievedPosts([FromQuery] string? search = null, [FromQuery] string? filter = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            var keycloakId = this.User.GetId();

            if (keycloakId == null)
            {
                return BadRequest();
            }

            return _mapper.Map<List<PostReadDto>>(await _postService.GetMessagesAsync(keycloakId, search, filter, limit, offset));
        }

        /// <summary>
        ///     Returns a list of posts that were sent as direct messages to the requesting user from the
        ///     specific user specified by id.
        /// </summary>
        /// <remarks>
        ///     Optionally accepts query parameters: search, filter, limit and offset.
        /// </remarks>
        /// <param name="search">Search posts by name.</param>
        /// <param name="filter">Sort posts by filters: descending or ascending.</param>
        /// <param name="limit">The maximum number of posts to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of posts within the overall list of posts, effectively skipping a certain number of posts.</param>
        [HttpGet("user/{id}")]
        public async Task<ActionResult<PostReadDto>> GetPostById(int id, [FromQuery] string? search = null, [FromQuery] string? filter = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            try
            {
                var posts = await _postService.GetPostByIdAsync(id, search, filter, limit, offset);
                var postDto = _mapper.Map<List<PostReadDto>>(posts);
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

        /// <summary>
        ///    Returns a list of groups that were sent with the group described by group_id as the target audience.
        /// </summary>
        /// <remarks>
        ///     Optionally accepts query parameters: search, filter, limit and offset.
        /// </remarks>
        /// <param name="search">Search posts by name.</param>
        /// <param name="filter">Sort posts by filters: descending or ascending.</param>
        /// <param name="limit">The maximum number of posts to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of posts within the overall list of posts, effectively skipping a certain number of posts.</param>
        [HttpGet("group/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetGroupPosts(int id, [FromQuery] string? search = null, [FromQuery] string? filter = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            try
            {
                return _mapper.Map<List<PostReadDto>>(await _postService.GetGroupPosts(id, search, filter, limit, offset));
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        /// <summary>
        ///    Returns a list of posts that were sent with the event described by postId as the target audience.
        /// </summary>
        /// <remarks>
        ///     Optionally accepts query parameters: search, filter, limit and offset.
        /// </remarks>
        /// <param name="search">Search posts by name.</param>
        /// <param name="filter">Sort posts by filters: descending or ascending.</param>
        /// <param name="limit">The maximum number of posts to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of posts within the overall list of posts, effectively skipping a certain number of posts.</param>
        [HttpGet("event/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetEventPosts(int id, [FromQuery] string? search = null, [FromQuery] string? filter = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            try
            {
                return _mapper.Map<List<PostReadDto>>(await _postService.GetEventPosts(id, search, filter, limit, offset));
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        /// <summary>
        ///    Returns a list of topics that were sent with the topic described by topicId as the target audience.
        /// </summary>
        /// <remarks>
        ///     Optionally accepts query parameters: search, filter, limit and offset.
        /// </remarks>
        /// <param name="search">Search posts by name.</param>
        /// <param name="filter">Sort posts by filters: descending or ascending.</param>
        /// <param name="limit">The maximum number of posts to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of posts within the overall list of posts, effectively skipping a certain number of posts.</param>
        [HttpGet("topic/{id}")]
        public async Task<ActionResult<IEnumerable<PostReadDto>>> GetTopicPosts(int id, [FromQuery] string? search = null, [FromQuery] string? filter = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            try
            {
                return _mapper.Map<List<PostReadDto>>(await _postService.GetTopicPosts(id, search, filter, limit, offset));
            }
            catch (PostNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }
        #endregion
        #region UPDATE
        /// <summary>
        ///     Updates a specified post. 
        /// </summary>
        /// <param name="id">Specify a post by its Id.</param>
        /// <remarks>
        ///     Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
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
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }


            //var existingPost = await _postService.GetPostByIdAsync(id);

            //if (existingPost.Where(p => p.RecieverId == postDto.RecieverId) == null)
            //{
            //    return Forbid("The audience of a post may not be changed after creation.");
            //}

            //try
            //{
            //    await _postService.UpdatePostAsync(_mapper.Map<Post>(postDto));
            //    return NoContent();
            //}
            //catch (PostNotFoundException ex)
            //{
            //    return NotFound(new ProblemDetails
            //    {
            //        Detail = ex.Message,
            //    });
            //}
        }

        #endregion
        #region CREATE
        /// <summary>
        ///     Updates a specified post. 
        /// </summary>
        /// <remarks>
        ///     Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
        // [TODO]: handle Attempts to post to an audience for which the requesting user is not a member will result in a 403 Forbidden response.
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
        /// <summary>
        ///     Updates a specified post. 
        /// </summary>
        /// <remarks>
        ///     Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
        // [TODO]: handle Attempts to post to an audience for which the requesting user is not a member will result in a 403 Forbidden response.
        [HttpPost("reply")]
        public async Task<ActionResult<Post>> AddReply(PostReplyCreateDto post)
        {
            string keycloakId = this.User.GetId();

            Post domainPost = _mapper.Map<Post>(post);
            domainPost.LastUpdated = DateTime.Now;
            try
            {
                domainPost = await _postService.AddReplyAsync(domainPost, keycloakId);
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

        #endregion
        #endregion
    }
}
