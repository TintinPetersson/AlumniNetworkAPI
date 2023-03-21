using AlumniNetworkAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly AlumniNetworkDbContext _context;

        public EventService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        #region Get

        public async Task<IEnumerable<Event>> GetEventsAsync(string keycloakId)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            return await _context.Events
                //.Where(e => e.AcceptedUsers.Any(u => u.Id == user.Id))
                .ToListAsync();
        }
        #endregion

        #region Add

        public async Task<Event> AddEventAsync(Event newEvent, string keycloakId)
        {
            User user = getUserByKeyCloakId(keycloakId);

            newEvent.LastUpdated = DateTime.Now;
            newEvent.AuthorId = user.Id;
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }
        #endregion

        #region Update
        public async Task UpdateEventAsync(Event newEvent, string keycloakId, int id)
        {
            Event updatedEvent = GetById(id).Result;

            if (newEvent.AllowGuests != null)
            {
                updatedEvent.AllowGuests = newEvent.AllowGuests;
            }
            if (newEvent.Description != null)
            {
                updatedEvent.Description = newEvent.Description;
            }
            if (newEvent.Name != null)
            {
                updatedEvent.Name = newEvent.Name;
            }
            if (newEvent.StartTime > DateTime.Now)
            {
                updatedEvent.StartTime = newEvent.StartTime;
            }
            if (newEvent.EndTime > DateTime.Now)
            {
                updatedEvent.EndTime = newEvent.EndTime;
            }

            User user = getUserByKeyCloakId(keycloakId);
            updatedEvent.LastUpdated = DateTime.Now;
            updatedEvent.AuthorId = user.Id;
            _context.Entry(updatedEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Create
        public async Task CreateGroupEventInvitation(int eventId, int groupId)
        {
           Event ev = _context.Events
                .Include(e => e.Groups)
                .FirstOrDefaultAsync(e => e.Id == eventId).Result;

            Group group = _context.Groups
                .Where(g => g.Id == groupId)
                .FirstOrDefaultAsync().Result;

            if (ev.Groups == null)
            {
                ev.Groups = new List<Group>();
            }

            ev.Groups.Add(group);
            await _context.SaveChangesAsync();
        }

        #endregion
        #region Helper-methods
        public async Task<Event> GetById(int id)
        {
            return await _context.Events
                 .Where(c => c.Id == id)
                 .FirstOrDefaultAsync();
        }
        public User getUserByKeyCloakId(string keycloakId)
        {
            User user = _context.Users
                .Include(u => u.Groups)
                .Include(u => u.Topics)
                .FirstOrDefaultAsync(u => u.KeycloakId == keycloakId).Result;
            return user;
        }

        public bool Exists(int id)
        {
            return _context.Events.Any(e => e.Id== id); 
        }

  
        #endregion
    }
}
