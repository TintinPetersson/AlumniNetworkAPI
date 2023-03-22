using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.EventServices
{
    public interface IEventService
    {
        /// <summary>
        /// Gets all the events asyncronously
        /// </summary>
        /// <param name="keycloakId">The sub identifier in the token</param>
        Task<IEnumerable<Event>> GetEventsAsync(string keycloakId);
        /// <summary>
        /// Add a new event to the list asyncronously
        /// </summary>
        /// <param name="newEvent">The new event to add</param>
        /// <param name="keycloakId">The sub identifier in the token</param>
        Task<Event> AddEventAsync(Event newEvent, string keycloakId);
        /// <summary>
        /// Update an existing event
        /// </summary>
        /// <param name="newEvent">The event to update</param>
        /// <param name="keycloakId">The sub identifier in the token</param>
        /// <param name="id">The ID of the event</param>
        Task UpdateEventAsync(Event newEvent, string keycloakId, int id);
        /// <summary>
        /// This methods creates a new event invitation to a group
        /// </summary>
        /// <param name="eventId">the Id of the event that we create a new invitation for</param>
        /// <param name="groupId">The id of the group that will get the invitation</param>
        Task CreateGroupEventInvitation(int eventId, int groupId);
        /// <summary>
        /// This method deletes an event invitation to a specifik group
        /// </summary>
        /// <param name="eventId">The ID of the event that we delete the invitation for</param>
        /// <param name="groupId">The ID of the group that will have an event invitation deleted</param>
        Task DeleteGroupEventInvitation(int eventId, int groupId);
        /// <summary>
        /// This method gives an event a topic
        /// </summary>
        /// <param name="eventId">the ID of the event we are creating an invitation for</param>
        /// <param name="topicId">the ID of the topic the event would have</param>
        Task CreateTopicEventInvitation(int eventId, int topicId);
        /// <summary>
        /// This method removes an existing event invitation for the event and the specified topic 
        /// </summary>
        /// <param name="eventId">The ID of the event we are removing the topic from</param>
        /// <param name="topicId">The Id of the topic we are removing</param>
        Task DeleteTopicEventInvitation(int eventId, int topicId);
        /// <summary>
        /// This method created an event invitation to a user
        /// </summary>
        /// <param name="eventId">The ID of the event the user is receiving an invitation for</param>
        /// <param name="userId">The ID of the user that is receiving the invitation</param>
        /// <returns></returns>
        Task CreateUserEventInvitation(int eventId, int userId);
        /// <summary>
        /// This method removes the event invitation sent to the user
        /// </summary>
        /// <param name="eventId">The ID of the event we are removing the invitation for</param>
        /// <param name="userId">The ID of the user we are removing from the invitation from</param>
        /// <returns></returns>
        Task DeleteUserEventInvitation(int eventId, int userId);
        /// <summary>
        /// This method creates an RSVP for the logged in user
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="keycloakId"></param>
        /// <returns></returns>
        Task CreateEventRSVP(int eventId, string keycloakId);
        /// <summary>
        /// This method checka if the event does exist or not
        /// </summary>
        /// <param name="id">The If of the event to check</param>
        public bool Exists(int id);
    }
}
