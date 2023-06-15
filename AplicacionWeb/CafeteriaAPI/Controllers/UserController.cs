﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service.Services;

namespace CafeteriaAPI.Controllers
{
    [Route("api/UserController")]
    [ApiController]
    public class UserController : ControllerBase
    {
       

        [HttpPost("CreateUser")]
        public ActionResult<UserDTO> CreateUsuario([FromBody] UserDTO usuario)
        {
            var response = _userService.CreateUsuario(usuario);

            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserDTO> GetUsuarioById(int id)
        {
            var response = _userService.GetUserById(id);

            return Ok(response);
        }

        [HttpGet("GetUserList")]
        public ActionResult<UserDTO> GetUserList()
        {
            var response = _userService.GetUserList();

            return Ok(response);
             
        }

        [HttpPut("PutModifiedUser/{id}")]

        public ActionResult<UserDTO> ModifyUser(int id, [FromBody] UserDTO user)
        {
            var response = _userService.ModifyUser(id, user);

            return Ok(response);
        }
    }
}
