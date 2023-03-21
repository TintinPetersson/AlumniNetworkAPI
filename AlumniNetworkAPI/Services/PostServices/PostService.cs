using AlumniNetworkAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly AlumniNetworkDbContext _context;
        public PostService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        #region READ
        public async Task<IEnumerable<Post>> GetPostsAsync(string keycloakId, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            User user = _context.Users.First(u => u.KeycloakId == keycloakId);

            var query = _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.ParentPostId == null)
                .Where(c => c.Group.Users.Contains(user) || c.Topic.Users.Contains(user));

            return await queryParameters(query, search, filter, limit, offset).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetMessagesAsync(string keycloakId, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            User user = _context.Users.First(u => u.KeycloakId == keycloakId);
            var query = _context.Posts.Where(p => p.RecieverId == user.Id);

            return await queryParameters(query, search, filter, limit, offset).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostByIdAsync(int id, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            var query = _context.Posts.Where(p => p.RecieverId == id && p.AuthorId == id);

            return await queryParameters(query, search, filter, limit, offset).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetGroupPosts(int groupId, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            var query = _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.GroupId == groupId)
                .Where(c => c.ParentPostId == null);

            return await queryParameters(query, search, filter, limit, offset).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetTopicPosts(int topicId, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            var query = _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.TopicId == topicId)
                .Where(c => c.ParentPostId == null);
            return await queryParameters(query, search, filter, limit, offset).ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetEventPosts(int eventId, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            var query = _context.Posts
                .Include(c => c.Group)
                .Include(c => c.Topic)
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(x => x.Author)
                .Where(c => c.EventId == eventId)
                .Where(c => c.ParentPostId == null);
            return await queryParameters(query, search, filter, limit, offset).ToListAsync();
        }
        #endregion
        #region UPDATE
        public async Task UpdatePostAsync(Post post)
        {
            post.LastUpdated = DateTime.Now;
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        #endregion
        #region CREATE
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
        #endregion
        #region Helper functions
        private static IQueryable<Post> queryParameters(IQueryable<Post> query, string? search = null, string? filter = null, int? limit = null, int? offset = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Title.Contains(search) || p.Body.Contains(search));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter.ToLower())
                {
                    case "ascending":
                        query = query.OrderBy(p => p.LastUpdated.Date).ThenBy(p => p.LastUpdated.TimeOfDay);
                        break;
                    case "descending":
                        query = query.OrderByDescending(p => p.LastUpdated.Date).ThenBy(p => p.LastUpdated.TimeOfDay);
                        break;
                    default:
                        throw new Exception("Invalid filter value");
                }
            }

            if (offset.HasValue)
                query = query.Skip(offset.Value);

            if (limit.HasValue)
                query = query.Take(limit.Value);
            return query;
        }
        #endregion
    }
}