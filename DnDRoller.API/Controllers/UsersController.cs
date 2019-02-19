using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDRoller.API.Domain.Helpers;
using DnDRoller.API.Domain.Services;
using DnDRoller.API.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DnDRoller.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Create([FromBody]UserModel user)
        {
            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[Action]")]
        public async Task<IActionResult> Authenticate([FromBody]string username, string password)
        {

            return StatusCode(200, "From Auth");
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
        public async Task<IActionResult> Update([FromBody]UserModel user)
        {
            return StatusCode(200);
        }
    }
}
