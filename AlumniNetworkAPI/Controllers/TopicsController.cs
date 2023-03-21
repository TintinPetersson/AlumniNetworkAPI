using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using AlumniNetworkAPI.Services.TopicServices;
using AlumniNetworkAPI.Helpers;
using AlumniNetworkAPI.Models.Dtos.Topics;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkAPI.CustomExceptions;
using System.Collections.Generic;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/topics")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public TopicsController(IMapper mapper, ITopicService topicService)
        {
            _mapper = mapper;
            _topicService = topicService;
        }
        #region CRUD
        #region READ
        /// <summary>
        /// Returns a list of topics.
        /// </summary>
        /// <remarks>
        /// Optionally accepts query parameters: search, limit and offset.
        /// </remarks>
        /// <param name="search">Search topics by name.</param>
        /// <param name="limit">The maximum number of topics to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of topics within the overall list of topics, effectively skipping a certain number of topics.</param>
        /// <returns>A list of topics.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicReadDto>>> GetTopics([FromQuery] string? search = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            try
            {
                return _mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsAsync(search, limit, offset));
            }
            catch (Exception)
            {
                return BadRequest("Bad Request");
            }
        }

        /// <summary>
        /// Returns specify topic by Id.
        /// </summary>
        /// <param name="id">Specify a topic by its Id.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TopicReadDto>>> GetTopicsById(int id)
        {
            try
            {
                return _mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsByIdAsync(id));
            }
            catch (TopicNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Bad Request");
            }
        }
        #endregion
        #region CREATE
        /// <summary>
        /// Creates a new topic.
        /// </summary>
        /// <remarks>
        /// Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Topic>> AddTopic(TopicCreateDto topic)
        {
            string keycloakId = this.User.GetId();
            Topic newTopic = _mapper.Map<Topic>(topic);
            try
            {
                newTopic = await _topicService.AddTopicAsync(newTopic, keycloakId);
                return CreatedAtAction("GetTopics", new { id = newTopic.Id }, _mapper.Map<TopicReadDto>(newTopic));
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid audience");
            }
            catch (Exception ex) 
            {
                return Forbid(ex.Message);
            }
        }

        /// <summary>
        /// Updates the topic members of a specified topic. 
        /// </summary>
        /// <param name="topicId">Specify a topic by its Id.</param>
        /// <remarks>
        /// Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
        [HttpPost]
        [Route("{topicId}/join")]
        public async Task<IActionResult> AddTopicUsers(int topicId)
        {
            string keycloakId = this.User.GetId();

            try
            {
                await _topicService.AddUserToTopicAsync(topicId, keycloakId);
                return Ok($"Subscribed user to topic");
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TopicNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }
        #endregion
        #endregion
    }
}
