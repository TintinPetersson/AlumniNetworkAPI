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
        
        #region CRUD with DTOs
        #region READ / Get
        // GET: api/v1/event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReadDto>>> GetEvents()
        {
            string keycloakId = this.User.GetId();
            return _mapper.Map<List<EventReadDto>>(await _eventService.GetEventsAsync(keycloakId));
        }

        #endregion

        #region POST  / Add / Create
        // POST: api/v1/event
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
        //POST: api/event/{eventId}/invite/group/{groupId}
        [HttpPost("{eventId}/invite/group/{groupId}")]
        public async Task<IActionResult> CreateGroupEventInvitation(int eventId, int groupId)
        {
            if(!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            try
            {
                await _eventService.CreateGroupEventInvitation(eventId, groupId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return NoContent();
        }

        #endregion 

        #region PUT / Update
        //PUT: api/events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventAsync(int id, EventEditDto ev)
        {
            string keycloakId = this.User.GetId();

            if (id != ev.Id)
            {
                return BadRequest();
            }
            if (!_eventService.Exists(id))
            {
                return NotFound();
            }
            Event domainEvent = _mapper.Map<Event>(ev);
            await _eventService.UpdateEventAsync(domainEvent, keycloakId, id);
            return NoContent();
        }

        #endregion


        #endregion
    }

}
