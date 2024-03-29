﻿using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Services.GroupServices
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync(string keycloakId, string search = null, int? limit = null, int? offset = null);
        Task<IEnumerable<Group>> GetGroupByIdAsync(string keycloakId, int id);
        Task<Group> AddGroupAsync(Group newGroup, string keycloakId);
        Task<Group> AddUserToGroupAsync(int groupId, string? keycloakId, int? userId);
    }
}
