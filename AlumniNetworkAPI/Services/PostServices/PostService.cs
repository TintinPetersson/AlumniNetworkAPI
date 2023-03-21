using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace AlumniNetworkAPI.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly AlumniNetworkDbContext _context;
        public PostService(AlumniNetworkDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPostsAsync(string keycloakId)
        {
            User user = _context.Users.First(u => u.KeycloakId == keycloakId);
            return await _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.ParentPostId == null)
                .Where(c => c.Group.Users.Contains(user) || c.Topic.Users.Contains(user))
                .OrderByDescending(c => c.LastUpdated.Date)
                .ThenBy(c => c.LastUpdated.TimeOfDay)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetMessagesAsync(string keycloakId)
        {
            User user = _context.Users.First(u => u.KeycloakId == keycloakId);
            return await _context.Posts
                .Where(c => c.RecieverId == user.Id)
                .OrderByDescending(c => c.LastUpdated.Date)
                .ThenBy(c => c.LastUpdated.TimeOfDay)
                .ToListAsync();
        }
        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts
                .Where(c => c.AuthorId == id)
                .Include(c => c.Author)
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Event)
                .OrderByDescending(c => c.LastUpdated.Date)
                .ThenBy(c => c.LastUpdated.TimeOfDay)
                .FirstOrDefaultAsync();

            return post;
        }
        public async Task<IEnumerable<Post>> GetGroupPosts(int groupId)
        {
            return await _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.GroupId == groupId)
                .Where(c => c.ParentPostId == null)
                .OrderByDescending(c => c.LastUpdated.Date)
                .ThenBy(c => c.LastUpdated.TimeOfDay)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetTopicPosts(int topicId)
        {
            return await _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.TopicId == topicId)
                .Where(c => c.ParentPostId == null)
                .OrderByDescending(c => c.LastUpdated.Date)
                .ThenBy(c => c.LastUpdated.TimeOfDay)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetEventPosts(int eventId)
        {
            return await _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.EventId == eventId)
                .Where(c => c.ParentPostId == null)
                .OrderByDescending(c => c.LastUpdated.Date)
                .ThenBy(c => c.LastUpdated.TimeOfDay)
                .ToListAsync();
        }
        public async Task UpdatePostAsync(Post post)
        {
            post.LastUpdated = DateTime.Now;
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<Post> AddPostAsync(Post domainPost, string keycloakId)
        {
            User user = _context.Users
            .Include(u => u.Groups)
            .Include(u => u.Topics)
            .Include(u => u.AcceptedEvents)
            .First(u => u.KeycloakId == keycloakId);

            try //[TODO]: Write better checks?, reciverId?
            {
                if (domainPost.TopicId != null)
                {
                    Topic audienceToPostTo = _context.Topics.First(t => t.Id == domainPost.TopicId);
                    ICollection<Topic>? audienceRelation = user.Topics;
                }
                if (domainPost.GroupId != null)
                {
                    var audienceToPostTo = _context.Groups.First(g => g.Id == domainPost.GroupId);
                    var audienceRelation = user.Groups; 
                }
                if (domainPost.EventId != null)
                {
                    Event audienceToPostTo = _context.Events.First(e => e.Id == domainPost.EventId);
                    ICollection<Event>? audienceRelation = user.AcceptedEvents;
                }
            }
            catch (Exception)
            {

                throw new KeyNotFoundException();
            }
          
            domainPost.AuthorId = user.Id;
            _context.Posts.Add(domainPost);
            await _context.SaveChangesAsync();
            return domainPost;
        }
    }
}