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
        /// This method checka if the event does exist or not
        /// </summary>
        /// <param name="id">The If of the event to check</param>
        public bool Exists(int id);
    }
}
