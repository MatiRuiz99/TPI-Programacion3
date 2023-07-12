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
        public ActionResult<UserDTO> CreateUsuario([FromBody] UserViewModel usuario)
        {
            
            try
            {
                var response = _authService.CrearUsuario(usuario);
                if (response == "Ingrese un usuario" || response == "Usuario existente")
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en CreateUser: {ex}");
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
                    return NotFound("email/contraseña incorrecta");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en CreateUser: {ex}");
                return BadRequest($"{ex.Message}");
            }

            
        }


        [HttpPost("CreateNewRole")]
        [Authorize]
        public ActionResult<RoleListViewModel> CreateNewRole([FromBody] RoleListViewModel newrole)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.CreateNewRole(newrole);
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en CreateNewRole: {ex}");
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
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetUserById: {ex}");
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
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetRoleList: {ex.Message}");
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
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en GetUserList: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("PutModifiedUser/{id}")]
        [Authorize]
        public ActionResult<UserDTO> ModifyUser(int id, [FromBody] UserViewModel user)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.ModifyUser(id, user);
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en PutModifiedUser: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize]
        public ActionResult<UserDTO> DeleteUser(int id)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value == "Administrador")
                {
                    var response = _service.DeleteUser(id);
                    return Ok(response);
                }
                throw new Exception("No tiene rol Administrador");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error en DeleteUser: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
