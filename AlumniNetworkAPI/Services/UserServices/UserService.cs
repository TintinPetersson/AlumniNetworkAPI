using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Users;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkAPI.Helpers;

namespace AlumniNetworkAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AlumniNetworkDbContext _context;
        public UserService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string keycloakId, string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.KeycloakId == keycloakId);

            if(user == null)
            {
                return await PostAsync(keycloakId, username);
            }
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            if(user == null)
            {
                throw new UserNotFoundException(id);
            }

            return user;
        }


        public async Task UpdateUserAsync(User patchUser, User userToPatch)
        {
            if (patchUser.Username != null)
            {
                userToPatch.Username = patchUser.Username;
            }
            if (patchUser.Status != null)
            {
                userToPatch.Status = patchUser.Status;
            }
            if (patchUser.Bio != null)
            {
                userToPatch.Bio = patchUser.Bio;
            }
            if (patchUser.FunFact != null)
            {
                userToPatch.FunFact = patchUser.FunFact;
            }
            if (patchUser.Picture != null)
            {
                userToPatch.Picture = patchUser.Picture;
            }
            _context.Entry(userToPatch).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<User> PostAsync(string keycloakId, string username)
        {
            User user = new User { KeycloakId = keycloakId, Username = username, Status = "" };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserInDb(string keycloakId)
        {
            return await _context.Users.AnyAsync(c => c.KeycloakId == keycloakId);
        }

        public User getUserFromKeyCloak(string keycloakId)
        {
            User user = _context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId).Result;
            return user;
        }
    }
}