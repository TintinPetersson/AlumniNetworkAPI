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

        //POST: api/event/{eventId}/invite/topic/{topicId}
        [HttpPost("{eventId}/invite/topic/{topicId}")]
        public async Task<IActionResult> CreateTopicEventInvitation(int eventId, int topicId)
        {
            if (!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            try
            {
                await _eventService.CreateTopicEventInvitation(eventId, topicId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return NoContent();
        }

        //POST: api/event/{eventId}/invite/user/{userId}
        [HttpPost("{eventId}/invite/user/{userId}")]
        public async Task<IActionResult> CreateUserEventInvitation(int eventId, int userId)
        {
            if (!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            try
            {
                await _eventService.CreateUserEventInvitation(eventId, userId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return NoContent();
        }

        //POST: api/event/{eventId}/rsvp
        [HttpPost("{eventId}/rsvp")]
        public async Task<IActionResult> CreateEventRSVP(int eventId)
        {
            if (!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            try
            {
                string keycloakId = this.User.GetId();
                await _eventService.CreateEventRSVP(eventId, keycloakId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return NoContent();
        }

        #endregion

        #region Delete
        //Delete: api/event/{eventId}/invite/group/{groupId}
        [HttpDelete("{eventId}/invite/group/{groupId}")]
        public async Task<IActionResult> DeleteGroupEventInvitation(int eventId, int groupId)
        {
            if (!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            await _eventService.DeleteGroupEventInvitation(eventId, groupId);

            return NoContent();
        }

        //Delete: api/event/{eventId}/invite/topic/{topicId}
        [HttpDelete("{eventId}/invite/topic/{topicId}")]
        public async Task<IActionResult> DeleteTopicEventInvitation(int eventId, int topicId)
        {
            if (!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            await _eventService.DeleteTopicEventInvitation(eventId, topicId);

            return NoContent();
        }


        //Delete: api/event/{eventId}/invite/user/{userId}
        [HttpDelete("{eventId}/invite/user/{userId}")]
        public async Task<IActionResult> DeletUserEventInvitation(int eventId, int userId)
        {
            if (!_eventService.Exists(eventId))
            {
                return NotFound();
            }
            await _eventService.DeleteUserEventInvitation(eventId, userId);

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
