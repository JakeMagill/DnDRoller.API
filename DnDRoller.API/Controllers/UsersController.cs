﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.Services;
using DnDRoller.API.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DnDRoller.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Create([FromBody]UserDTO user)
        {
            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Authenticate([FromBody]string username, string password)
        {
            var user = _userService.Authenticate(username, password);

            if(user == null)
            {
                return BadRequest(new {message = "Username or Password incorrect"});
            }
            
            return Ok(user);
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
