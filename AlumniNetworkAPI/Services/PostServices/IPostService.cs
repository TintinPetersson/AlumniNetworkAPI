using AlumniNetworkAPI.Models.Domain;
using Microsoft.CodeAnalysis.Operations;

namespace AlumniNetworkAPI.Services.PostServices
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(string keycloakId);
        Task<IEnumerable<Post>> GetMessagesAsync(string keycloakId);
        Task<IEnumerable<Post>> GetPostByIdAsync(int id, string keycloakId);
        Task<IEnumerable<Post>> GetGroupPosts(int groupId);
        Task<IEnumerable<Post>> GetTopicPosts(int topicId);
        Task<IEnumerable<Post>> GetEventPosts(int eventId);
        Task UpdatePostAsync(Post post);

        Task<Post> AddPostAsync(Post post, string keycloakId);
    }
}
