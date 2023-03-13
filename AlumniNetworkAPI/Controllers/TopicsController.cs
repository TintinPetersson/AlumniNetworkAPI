using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using AlumniNetworkAPI.Services.TopicServices;
using AlumniNetworkAPI.Helpers;
using AlumniNetworkAPI.Models.Dtos.Topics;
using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/[controller]")]
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
            string? keycloakId = this.User.GetId();

            if (keycloakId == null)
            {
                return BadRequest();
            }
            return _mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsAsync(keycloakId));
        }

        // GET: api/v1/Topics/id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TopicReadDto>>> GetTopicsById(int id)
        {
            if (_mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsByIdAsync(id)) != null)
            {
                Console.WriteLine(_mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsByIdAsync(id))); // [TODO]: Fix so it throws a error
                return _mapper.Map<List<TopicReadDto>>(await _topicService.GetTopicsByIdAsync(id));
            }
            
            else
                throw new TopicNotFoundException(id); 
        
        }
    }
}
