using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModel;
using Service.IServices;
using Service.Services;

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
            var response = _service.CreateUsuario(usuario);

            return Ok(response);
        }

        [HttpPost("CreateNewRole")]
        public ActionResult<RoleListViewModel> CreateNewRole([FromBody] RoleListViewModel newrole)
        {
            var response = _service.CreateNewRole(newrole);

            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<UserDTO> GetUsuarioById(int id)
        {
            var response = _service.GetUserById(id);

            return Ok(response);
        }

        [HttpGet("GetRoleList")]
        public ActionResult<List<RoleListDTO>> GetRoleList()
        {
            var response = _service.GetRoleList();

            return Ok(response);

        }

        [HttpGet("GetUserList")]
        public ActionResult<List<UserxRoleDTO>> GetUserList()
        {
            var response = _service.GetUserList();

            return Ok(response);
             
        }

        [HttpPut("PutModifiedUser/{id}")]

        public ActionResult<UserDTO> ModifyUser(int id, [FromBody] UserDTO user)
        {
            var response = _service.ModifyUser(id, user);

            return Ok(response);
        }
    }
}
