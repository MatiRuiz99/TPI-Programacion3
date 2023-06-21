using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Services
{
    public class UserService :IUserService
    {
        private readonly CafeteriaContext _context;

        public UserService(CafeteriaContext context)
        {
            _context = context;
        }

        public string CreateUsuario(UserViewModel usuario)
        {
            string response = string.Empty;

            var role = _context.RoleList.FirstOrDefault(f => f.Id == usuario.RoleId);

            if (role != null)
            {
                _context.Users.Add(new Users()
                {
                    Email = usuario.Email,
                    Name = usuario.Name,
                    RoleId = role.Id

                });
                _context.SaveChanges();
                response = "Registro de usuario exitoso";
            }
            else
            {
                response = "Error al agregar el usuario (rol no existente)";
            }
            return response;
        }

        public string CreateNewRole(RoleListViewModel newrole)
        {
            var existingRole = _context.RoleList.FirstOrDefault(r => r.Authority.ToLower() == newrole.Authority.ToLower());

            if (existingRole == null)
            {
                var role = new RoleList()
                {
                    Authority = newrole.Authority
                };

                _context.RoleList.Add(role);
                _context.SaveChanges();
                return "Nuevo rol añadido exitosamente";
            }
            else
            {
                return "Error al añadir el nuevo rol (el rol ya existe)";
            }
        }

        public List<RoleList> GetRoleList()
        {
            var roleList = _context.RoleList.ToList();

            return roleList;
        }

        public Users GetUserById(int id)
        {
            var usuario = _context.Users.FirstOrDefault(u => u.UserId == id);

            return usuario;
        }

        public List<UserxRoleDTO> GetUserList()
        {
            List<UserxRoleDTO> userList = (from u in _context.Users
                                      join r in _context.RoleList
                                      on u.RoleId equals r.Id
                                      select new UserxRoleDTO()
                                      {
                                          UserId = u.UserId,
                                          Name = u.Name,
                                          Email = u.Email,
                                          Role = r.Authority,

                                      }).ToList();

            return userList;
        }

        public string ModifyUser(int id, UserDTO usuarioModificado)
        {
            var usuario = _context.Users.FirstOrDefault(u => u.UserId == id);
            var role = _context.RoleList.FirstOrDefault(r => r.Id == usuarioModificado.RoleId);

            if (usuario != null && role != null)
            {
                usuario.Email = usuarioModificado.Email;
                usuario.Name = usuarioModificado.Name;
                usuario.RoleId = role.Id;

                _context.SaveChanges();
                return "Usuario modificado exitosamente";
            }
            else if (usuario == null)
            {
                return "Usuario no encontrado";
            }
            else
            {
                return "Error al modificar el usuario (rol no existente)";
            }
        }
    }
}
