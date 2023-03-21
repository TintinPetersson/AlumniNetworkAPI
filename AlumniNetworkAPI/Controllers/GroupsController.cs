using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using AlumniNetworkAPI.Services.GroupServices;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AlumniNetworkAPI.Helpers;
using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Models.Domain;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/groups")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IMapper mapper, IGroupService groupService)
        {
            _mapper = mapper;
            _groupService = groupService;
        }
        #region CRUD
        #region READ
        /// <summary>
        /// Returns a list of groups.
        /// </summary>
        /// <remarks>
        /// Optionally accepts query parameters: search, limit and offset.
        /// </remarks>
        /// <param name="search">Search groups by name.</param>
        /// <param name="limit">The maximum number of groups to return in the response.</param>
        /// <param name="offset">Specify the starting point of a subset of groups within the overall list of groups, effectively skipping a certain number of groups.</param>
        /// <returns>A list of groups.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupReadDto>>> GetGroups([FromQuery] string? search = null, [FromQuery] int? limit = null, [FromQuery] int? offset = null)
        {
            string keycloakId = this.User.GetId();
            var groups = await _groupService.GetGroupsAsync(keycloakId, search, limit, offset);
            return _mapper.Map<List<GroupReadDto>>(groups);
        }

        /// <summary>
        /// Returns a group.
        /// </summary>
        /// <param name="id">Specify a group by its Id.</param>
        /// <returns>The group that corresponds to given Id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GroupReadDto>>> GetGroupById(int id)
        {
            string keycloakId = this.User.GetId();

            try
            {
                return _mapper.Map<List<GroupReadDto>>(await _groupService.GetGroupByIdAsync(keycloakId, id));
            }
            catch (NoAccessToGroupException ex)
            {
                return Forbid();
            }
            catch (Exception) 
            {
                return NotFound();
            }
        }
        #endregion
        #region CREATE
        /// <summary>
        /// Creates a new group. 
        /// </summary>
        /// <remarks>
        /// Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Group>> AddTopic(GroupCreateDto group)
        {
            string keycloakId = this.User.GetId();
            Group newGroup = _mapper.Map<Group>(group);
            try
            {
                newGroup = await _groupService.AddGroupAsync(newGroup, keycloakId);
                return CreatedAtAction("GetGroups", new { id = newGroup.Id }, _mapper.Map<GroupReadDto>(newGroup));
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid audience");
            }
            catch (Exception)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Updates the group members of a specified group. 
        /// </summary>
        /// <param name="groupId">Specify a group by its Id.</param>
        /// <param name="userId">Specify a user by its Id.</param>
        ///  <remarks>
        /// Accepts appropriate parameters in the request body as application/json, if parameter "userId" is left empty the userId is taken form the user that calling the endpoint! 
        /// </remarks>
        [HttpPost]
        [Route("{groupId}/join")]
        public async Task<IActionResult> AddGroudUsers(int groupId, int? userId)
        {
            string keycloakId = null;

            if (userId == null)
            {
                keycloakId = this.User.GetId();
            }
            
            try
            {
                await _groupService.AddUserToGroupAsync(groupId, keycloakId, userId);
                return Ok($"User added to group");
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Invalid audience");
            }
            catch (ArgumentException)
            {
                return Conflict();
            }
            catch (NoAccessToGroupException)
            {
                return Forbid();
            }
        }
        #endregion
        #endregion
    }
}
