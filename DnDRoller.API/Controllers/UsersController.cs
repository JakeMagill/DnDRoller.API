using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DnDRoller.API.Application.Interfaces;
using DnDRoller.API.Application.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DnDRoller.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Create([FromBody]UserDTO userDTO)
        {
            var returnUser = await _userService.Create(userDTO);

            if(returnUser == null)
            {
                return BadRequest("Failed to create user");
            }

            return StatusCode(201, new {
                returnUser.Id
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Authenticate([FromHeader]string username, [FromHeader]string password)
        {
            var returnUser = await _userService.Authenticate(username, password);

            if(returnUser == null)
            {
                return BadRequest(new {message = "Username or Password incorrect"});
            }

            return StatusCode(201, new {
                returnUser.Id,
                returnUser.token
            });
        }

        [HttpGet]
        [Authorize]
        [Route("[Action]")]
        public async Task<IActionResult> Details([FromHeader]Guid id)
        {
            var returnUser = await _userService.Details(id);

            if (returnUser == null)
            {
                return BadRequest(new { message = "User details could not be retrieved" });
            }

            return StatusCode(200, returnUser);
        }

        [HttpDelete]
        [Authorize]
        [Route("[Action]")]
        public async Task<IActionResult> Delete([FromHeader]Guid id)
        {
            var returnUser = await _userService.Delete(id);

            if (returnUser == false)
            {
                return BadRequest(new { message = "User could not be removed" });
            }

            return StatusCode(202);
        }

        [HttpPut]
        [Authorize]
        [Route("[Action]")]
        public async Task<IActionResult> Update([FromBody]UserDTO user)
        {
            UserDTO returnUser = await _userService.Update(user);

            if (returnUser == null)
            {
                return BadRequest(new { message = "User could not be updated" });
            }

            return StatusCode(202);
        }
    }
}
