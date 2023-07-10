using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;

namespace CafeteriaAPI.Controllers
{
    [Route("api/UserController")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("CreateUser")]
        public ActionResult<UserDTO> CreateUsuario([FromBody] UserViewModel usuario)
        {
            try
            {
                var response = _service.CreateUsuario(usuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateNewRole")]
        public ActionResult<RoleListViewModel> CreateNewRole([FromBody] RoleListViewModel newrole)
        {
            try
            {
                var response = _service.CreateNewRole(newrole);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserDTO> GetUsuarioById(int id)
        {
            try
            {
                var response = _service.GetUserById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetRoleList")]
        public ActionResult<List<RoleListDTO>> GetRoleList()
        {
            try
            {
                var response = _service.GetRoleList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetUserList")]
        public ActionResult<List<UserxRoleDTO>> GetUserList()
        {
            try
            {
                var response = _service.GetUserList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("PutModifiedUser/{id}")]
        public ActionResult<UserDTO> ModifyUser(int id, [FromBody] UserViewModel user)
        {
            try
            {
                var response = _service.ModifyUser(id, user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public ActionResult<UserDTO> DeleteUser(int id)
        {
            try
            {
                var response = _service.DeleteUser(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
