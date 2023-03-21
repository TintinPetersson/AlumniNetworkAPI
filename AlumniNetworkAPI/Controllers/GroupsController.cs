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

        // GET: api/v1/groups
        [HttpGet] // [TODO]: Add Query parameters!
        public async Task<ActionResult<IEnumerable<GroupReadDto>>> GetGroups()
        {
            string keycloakId = this.User.GetId();
            return _mapper.Map<List<GroupReadDto>>(await _groupService.GetGroupsAsync(keycloakId));
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GroupReadDto>> GetGroupById(int id)
        {
            try
            {
                return _mapper.Map<GroupReadDto>(await _groupService.GetGroupByIdAsync(id));
            }
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/v1/group
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
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
        }

        /*  [TODO]: Get Task describe below to work!
            If the group for which the membership record is being created is private, then only current members of the group may create group member records for that group. 
            Attempts to do so by non-members will result in a 403 Forbidden response.
         */
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
            catch (Exception)
            {
                return Forbid();
            }
        }
    }
}
