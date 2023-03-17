using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using AlumniNetworkAPI.Helpers;
using AutoMapper;
using AlumniNetworkAPI.Services.EventServices;
using AlumniNetworkAPI.Models.Dtos.Events;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/event")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IMapper mapper, IEventService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        // GET: api/v1/event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReadDto>>> GetEvents()
        {
            string keycloakId = this.User.GetId();
            return _mapper.Map<List<EventReadDto>>(await _eventService.GetEventsAsync(keycloakId));
        }

        // POST: api/v1/group
        [HttpPost]
        public async Task<ActionResult<Event>> AddEvent(EventCreateDto dtoEvent)
        {
            string keycloakId = this.User.GetId();
            Event newEvent = _mapper.Map<Event>(dtoEvent);

            try
            {
                newEvent = await _eventService.AddEventAsync(newEvent, keycloakId);
                return CreatedAtAction("GetEvents", new { id = newEvent.Id }, _mapper.Map<EventReadDto>(newEvent));
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
