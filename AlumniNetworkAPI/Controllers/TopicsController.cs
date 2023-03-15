using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using AlumniNetworkAPI.Services.TopicServices;
using AlumniNetworkAPI.Helpers;
using AlumniNetworkAPI.Models.Dtos.Topics;
using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/topic")]
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

        // GET: api/v1/Topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicReadDto>>> GetTopics()
        {
            return _mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsAsync());
        }

        // GET: api/v1/Topics/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TopicReadDto>>> GetTopicsById(int id)
        {
            //Check if id is valid and if not return topic with id not found!
            return _mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsByIdAsync(id));

        }

        // POST: api/v1/Topics
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
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid audience");
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

    }
}
