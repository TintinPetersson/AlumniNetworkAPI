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

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<UserReadDto>> GetUser()
        {
            string? keycloakId = this.User.GetId();

            if(keycloakId == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<UserReadDto>(await _userService.GetUserAsync(keycloakId)));
        }

        // GET: api/Users/5
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

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserEditDto userDto)
        {
            try
            {
                await _userService.UpdateUserAsync(_mapper.Map<User>(userDto));
                return NoContent();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new ProblemDetails
                {
                    Detail = ex.Message,
                });
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser()
        {

            var keycloakID = this.User.GetId();
            var username = this.User.GetUsername();

            if (keycloakID == null || username == null)
            {
                return NotFound();
            }

            if (_userService.GetUserAsync(keycloakID) != null)
            {
                return BadRequest();
            }

            User domainUser = await _userService.PostAsync(keycloakID, username);

            return CreatedAtAction("GetUser",
                new { id = domainUser.Id },
                _mapper.Map<UserReadDto>(domainUser));
        }
    }
}
