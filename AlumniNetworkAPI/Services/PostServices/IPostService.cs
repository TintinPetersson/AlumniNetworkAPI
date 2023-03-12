using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Posts;

namespace AlumniNetworkAPI.Services.PostServices
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(string keycloakId);
        Task<Post> GetPostByIdAsync(int id);
        Task UpdatePostAsync(Post post);
        Task<Post> AddPostAsync(Post post, string keycloakId);
    }
}
