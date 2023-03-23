using AlumniNetworkAPI.Models.Domain;
using Microsoft.CodeAnalysis.Operations;

namespace AlumniNetworkAPI.Services.PostServices
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(string keycloakId, string? search = null, string? filter = null, int? limit = null, int? offset = null);
        Task<IEnumerable<Post>> GetMessagesAsync(string keycloakId, string search = null, string? filter = null, int? limit = null, int? offset = null);
        Task<IEnumerable<Post>> GetPostByIdAsync(int id, string? search = null, string? filter = null, int? limit = null, int? offset = null);
        Task<IEnumerable<Post>> GetGroupPosts(int groupId, string? search = null, string? filter = null, int? limit = null, int? offset = null);
        Task<IEnumerable<Post>> GetTopicPosts(int topicId, string? search = null, string? filter = null, int? limit = null, int? offset = null);
        Task<IEnumerable<Post>> GetEventPosts(int eventId, string? search = null, string? filter = null, int? limit = null, int? offset = null);
        Task UpdatePostAsync(Post post);
        Task<Post> AddPostAsync(Post post, string keycloakId);
        Task<Post> AddReplyAsync(Post post, string keycloakId);
    }
}
