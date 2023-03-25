using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AlumniNetworkAPI.Models.Domain;
using AlumniNetworkAPI.Services.UserServices;
using AutoMapper;
using System.Net.Mime;
using AlumniNetworkAPI.Models.Dtos.Users;
using AlumniNetworkAPI.Helpers;
using AlumniNetworkAPI.CustomExceptions;
using AlumniNetworkAPI.Dtos;
using AlumniNetworkAPI.Models.Dtos.Groups;
using AlumniNetworkAPI.Models.Dtos.Topics;

namespace AlumniNetworkAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        #region CRUD
        #region READ
        /// <summary>
        /// Returns user id of user that is calling the endpoint and a corresponding redirect link.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
                string? keycloakId = this.User.GetId();
                var username = this.User.GetUsername();

                if (keycloakId == null || username == null)
                {
                    return BadRequest();
                }

                var user = await _userService.GetUserAsync(keycloakId, username);

                var profileUrl = $"https://localhost:7240/api/v1/Users/{user.Id}";

                HttpContext.Response.Headers.Add("Location", profileUrl);
                return StatusCode(303);
        }
 
        /// <summary>
        /// Returns user id of user that is calling the endpoint and a corresponding redirect link.
        /// </summary>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserReadGroupDto>>> GetUsersByNames()
        {
            try
            {
                return _mapper.Map<List<UserReadGroupDto>>(await _userService.GetUsersByName());
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

        /// <summary>
        /// Returns user data of specify user.
        /// </summary>
        /// <param name="id">Specify a user by its Id.</param>
        /// <returns>The user that corresponds to given Id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(int id)
        {
            try
            {
                User user = await _userService.GetUserByIdAsync(id);
                var userDto = _mapper.Map<UserReadDto>(user);
                return Ok(userDto);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }
        #endregion
        #region UPDATE
        /// <summary>
        /// Edit user data of specify user.
        /// </summary>
        /// <param name="id">Specify a user by its Id.</param>
        /// /// <remarks>
        /// Accepts appropriate parameters in the request body as application/json.
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserEditDto userInput)
        {
            string keycloakId = this.User.GetId();
            User userToPatch = _userService.getUserFromKeyCloak(keycloakId);

            if (userToPatch.Id != id)
            {
                return BadRequest();
            }

            User patchUser = _mapper.Map<User>(userInput);
            await _userService.UpdateUserAsync(patchUser, userToPatch);

            return Ok(userToPatch);
        }
        #endregion
        #endregion
    }
}