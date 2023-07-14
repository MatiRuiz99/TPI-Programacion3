using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CafeteriaAPI.Controllers
{
    [Route("api/UserController")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IAuthService _authService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger, IAuthService authService)
        {
            _service = service;
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("CreateUser")]
        public ActionResult<UserDTO> CreateUser([FromBody] UserViewModel usuario)
        {
            
            try
            {
                var response = _authService.CreateUser(usuario);
                if (response == "User can't be empty" || response == "User already in use")
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in CreateUser: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] AuthViewModel User)
        {
            string response = string.Empty;
            try
            {
                response = _authService.Login(User);
                if (string.IsNullOrEmpty(response))
                {
                    return NotFound("Incorrect Email/Password");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in CreateUser: {ex}");
                return BadRequest($"{ex.Message}");
            }

            
        }


        [HttpPost("CreateNewRole")]
        
        public ActionResult<string> CreateNewRole([FromBody] RoleListViewModel newrole)
        {
            try
            {
                var response = _service.CreateNewRole(newrole);
                if (response == null)
                {
                    return BadRequest("Error adding new role (role already exists)");
                }
                    return Ok(response);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in CreateNewRole: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize]
        public ActionResult<UserDTO> GetUsuarioById(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.GetUserById(id);
                    if (response == null)
                    {
                       return NotFound("User Not Found");
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetUserById: {ex}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetRoleList")]
        [Authorize]
        public ActionResult<List<RoleListDTO>> GetRoleList()
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.GetRoleList();
                    if (response.Count == 0)
                    {
                        return NotFound("Role List is empty");
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetRoleList: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetUserList")]
        [Authorize]
        public ActionResult<List<UserxRoleDTO>> GetUserList()
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.GetUserList();
                    if (response.Count == 0)
                    {
                        return NotFound("User List is empty");
                    }
                    return Ok(response);
                    
                }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in GetUserList: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("PutModifiedUser/{id}")]
        [Authorize]
        public ActionResult<string> ModifyUser(int id, [FromBody] UserViewModel user)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.ModifyUser(id, user);
                    if (response == "Error modifying user (non-existent role)")
                    {
                        BadRequest(response);
                    }else if (response == "User not found")
                    {
                        NotFound(response);
                    }
                    return Ok(response);
                }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in PutModifiedUser: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize]
        public ActionResult<string> DeleteUser(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.DeleteUser(id);
                    if (response == "User not found")
                    {
                        NotFound(response);
                    }

                    return Ok(response);
                 }
                throw new Exception("You don't have an Administrator role");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in DeleteUser: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
