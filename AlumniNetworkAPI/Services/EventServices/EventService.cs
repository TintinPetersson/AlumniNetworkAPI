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

        #region Read
        public async Task<IEnumerable<Event>> GetEventsAsync(string keycloakId)
        {
            User user = getUserByKeyCloakId(keycloakId);

            return await _context.Events
                .Include(e => e.Groups)
                //.Include(e => e.Posts).ThenInclude(p => p.Author)
                .Include(e => e.AcceptedUsers)
                .Include(e => e.InvitedUsers)
                .Include(e => e.Author)
                .Where(e => e.Groups.Any(g => user.Groups.Contains(g)))
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
           Event ev = await _context.Events
                .Include(e => e.Groups)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            Group group = await _context.Groups
                .Where(g => g.Id == groupId)
                .FirstOrDefaultAsync();

            if (ev.Groups == null)
            {
                ev.Groups = new List<Group>();
            }

            ev.Groups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTopicEventInvitation(int eventId, int topicId)
        {
            Event ev = await _context.Events
                 .Include(e => e.Topics)
                 .FirstOrDefaultAsync(e => e.Id == eventId);

            Topic topic = await _context.Topics
                .Where(g => g.Id == topicId)
                .FirstOrDefaultAsync();

            if (ev.Topics == null)
            {
                ev.Topics = new List<Topic>();
            }

            ev.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUserEventInvitation(int eventId, int userId)
        {
            Event ev = await _context.Events
                .Include(e => e.InvitedUsers)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            User user = await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();   
            
            if (ev.InvitedUsers == null)
            {
                ev.InvitedUsers = new List<User>();
            }

            ev.InvitedUsers.Add(user);
            await _context.SaveChangesAsync();
        }
        #region Delete
        public async Task DeleteGroupEventInvitation(int eventId, int groupId)
        {
            Event ev = await _context.Events
                 .Include(e => e.Groups)
                 .FirstOrDefaultAsync(e => e.Id == eventId);

            Group group = await _context.Groups
                .Where(g => g.Id == groupId)
                .FirstOrDefaultAsync();

            ev.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTopicEventInvitation(int eventId, int topicId)
        {
            Event ev = await _context.Events
                 .Include(e => e.Topics)
                 .FirstOrDefaultAsync(e => e.Id == eventId);

            Topic topic = await _context.Topics
                .Where(t => t.Id == topicId)
                .FirstOrDefaultAsync();

            ev.Topics.Remove(topic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserEventInvitation(int eventId, int userId)
        {
            Event ev = await _context.Events
                 .Include(e => e.InvitedUsers)
                 .FirstOrDefaultAsync(e => e.Id == eventId);

            User user = await _context.Users
                .Where(t => t.Id == userId)
                .FirstOrDefaultAsync();

            ev.InvitedUsers.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreateEventRSVP(int eventId, string keycloakId)
        {
            Event ev = await _context.Events
                .Include(e => e.InvitedUsers)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            User user = await _context.Users
                .Where(u => u.KeycloakId == keycloakId)
                .FirstOrDefaultAsync();

            if (ev.AcceptedUsers == null)
            {
                ev.AcceptedUsers = new List<User>();
            }

            ev.AcceptedUsers.Add(user);
            await _context.SaveChangesAsync();
        }
        #endregion

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
