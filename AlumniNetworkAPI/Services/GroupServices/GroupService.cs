﻿using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace AlumniNetworkAPI.Services.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly AlumniNetworkDbContext _context;

        public GroupService(AlumniNetworkDbContext context)
        {
            _context = context;
        }
        #region READ
        public async Task<IEnumerable<Group>> GetGroupsAsync(string keycloakId, string search = null, int? limit = null, int? offset = null)
        {

            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);

            var query = _context.Groups
                .Include(g => g.Users)
                .Include(g => g.Posts)
                .Where(g => g.Users.Any(u => u.Id == user.Id) || g.IsPrivate == false);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(g => g.Name.Contains(search));

            if (offset.HasValue)
                query = query.Skip(offset.Value);
            
            if (limit.HasValue)
                query = query.Take(limit.Value);

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Group>> GetGroupByIdAsync(string keycloakId, int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            Group group = _context.Groups
                .Include(g => g.Users)
                .Include(g => g.Posts)
                .FirstOrDefault(g => g.Id == id);

             
            if (group.IsPrivate && !group.Users.Any(u => u.Id == user.Id))
                throw new NoAccessToGroupException(user.Id, group.Id);
            
            return await _context.Groups
               .Where(g => g.Id == id).ToListAsync();
        }
        #endregion
        #region CREATE
        public async Task<Group> AddGroupAsync(Group newGroup, string keycloakId)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);

            var users = new List<User> { user };

            foreach (var userInGroup in newGroup.Users)
            {
                if (user.Id != userInGroup.Id)
                {
                    var userI = await _context.Users.FindAsync(userInGroup.Id);
                    if (userI != null)
                        users.Add(userI);
                    
                    else
                        throw new Exception($"User with id {userInGroup.Id} does not exist.");        
                }
            }

            newGroup.Users = users;
            await _context.Groups.AddAsync(newGroup);
            await _context.SaveChangesAsync();
            return newGroup;
        }

        public async Task<Group> AddUserToGroupAsync(int groupId, string? keycloakId, int? userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId); 
            
            var group = await _context.Groups.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == groupId);
           

            if (group.IsPrivate && group.Users.Any(u => u.Id == user.Id))
                throw new NoAccessToGroupException(user.Id, group.Id);
           
            group.Users.Add(user);
            await _context.SaveChangesAsync();
            return group;
        }
        #endregion
    }
}
