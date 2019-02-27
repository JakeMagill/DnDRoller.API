using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.Services;
using DnDRoller.API.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DnDRoller.API.Domain.Entities;

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
            User user = _mapper.Map<User>(userDTO);
            var returnUser = await _userService.Create(user, userDTO.Password);

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
                returnUser.Id
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Details([FromHeader]Guid id)
        {
            return StatusCode(200);
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Delete([FromHeader]Guid id)
        {
            return StatusCode(200);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Update([FromBody]UserDTO user)
        {
            return StatusCode(200);
        }
    }
}
