﻿using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Models.Dtos.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniNetworkAPI.Services.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly AlumniNetworkDbContext _context;

        public GroupService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(string keycloakId)
        {
            //return await _context.Groups.ToListAsync();
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            return await _context.Groups
                .Where(g => g.Users.Any(u => u.Id == user.Id) || g.IsPrivate == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupByIdAsync(string keycloakId, int id)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            Group group = _context.Groups.Include(g => g.Users).FirstOrDefault(g => g.Id == id);

             
            if (group.IsPrivate && !group.Users.Any(u => u.Id == user.Id))
            {
                throw new NoAccessToGroupException(user.Id, group.Id);
            }
            return await _context.Groups
               .Where(g => g.Id == id).ToListAsync();
        }

        public async Task<Group> AddGroupAsync(Group newGroup, string keycloakId)
        {
            User user = _context.Users.FirstOrDefault(u => u.KeycloakId == keycloakId);
            var users = new List<User>
            {
                user
            };

            newGroup.Users = users;
            await _context.Groups.AddAsync(newGroup);
            await _context.SaveChangesAsync();
            return newGroup;
        }

        public async Task AddUserToGroupAsync(int groupId, string? keycloakId, int? userId)
        {
            var user = new User();
            if (userId == null)
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId);
            }
            else
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            }

            var group = await _context.Groups.Include(t => t.Users).FirstOrDefaultAsync(t => t.Id == groupId);
           

            if (group.IsPrivate && !group.Users.Any(u => u.Id == user.Id))
            {
                throw new NoAccessToGroupException(user.Id, group.Id);
            }

            group.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
