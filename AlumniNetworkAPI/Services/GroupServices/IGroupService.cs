﻿using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.GroupServices
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync(string keycloakId);
        Task<Group> GetGroupByIdAsync(int id);
        Task<Group> AddGroupAsync(Group newGroup, string keycloakId);
        Task AddUserToGroupAsync(int groupId, string? keycloakId, int? userId);
    }
}
