using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Users;

namespace AlumniNetworkAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string keycloakId, string username);
        Task<IEnumerable<User>> GetUsersByName();
        Task<User> GetUserByIdAsync(int id);
        Task UpdateUserAsync(User patchUser, User userToPatch);
        Task<User> PostAsync(string keycloakId, string username);
        Task<bool> UserInDb(string keycloakId);
        public User getUserFromKeyCloak(string keycloakId);
    }
}
