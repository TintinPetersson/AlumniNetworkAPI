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
    [Route("api/v1/Event")]
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
        /// <summary>
        /// Gets all the events.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventReadDto>>> GetEvents()
        {
            return _mapper.Map<List<EventReadDto>>(await _eventService.GetEventsAsync());
        }
        #endregion

        #region POST  / Add / Create
        /// <summary>
        /// Add a new event to the list
        /// </summary>
        /// <remarks>
        /// Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
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

        /// <summary>
        /// This methods creates a new event invitation to a group CreateGroupEventInvitation
        /// </summary>
        /// <param name="eventId">the Id of the event that we create a new invitation for</param>
        /// <param name="groupId">The id of the group that will get the invitation</param>
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

        /// <summary>
        /// This method gives an event a topic
        /// </summary>
        /// <param name="eventId">the ID of the event we are creating an invitation for</param>
        /// <param name="topicId">the ID of the topic the event would have</param>
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

        /// <summary>
        /// This method created an event invitation to a user
        /// </summary>
        /// <param name="eventId">The ID of the event the user is receiving an invitation for</param>
        /// <param name="userId">The ID of the user that is receiving the invitation</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method creates an RSVP for the logged in user
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method deletes an event invitation to a specific group
        /// </summary>
        /// <param name="eventId">The ID of the event that we delete the invitation for</param>
        /// <param name="groupId">The ID of the group that will have an event invitation deleted</param>
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

        /// <summary>
        /// This method removes an existing event invitation for the event and the specified topic 
        /// </summary>
        /// <param name="eventId">The ID of the event we are removing the topic from</param>
        /// <param name="topicId">The Id of the topic we are removing</param>
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


        /// <summary>
        /// This method removes the event invitation sent to the user
        /// </summary>
        /// <param name="eventId">The ID of the event we are removing the invitation for</param>
        /// <param name="userId">The ID of the user we are removing from the invitation from</param>
        /// <returns></returns>
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

        /// <summary>
        /// Update an existing event
        /// </summary>
        /// <param name="id">The ID of the event</param>
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
