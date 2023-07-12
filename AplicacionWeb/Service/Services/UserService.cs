using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.IServices;
using Service.Mappings;
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
        private readonly IMapper _mapper;

        public UserService(CafeteriaContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public string CreateUsuario(UserViewModel usuario)
        {
            string response = string.Empty;
            var role = _context.RoleList.First(f => f.Id == usuario.RoleId);
            if (role != null)
            {               
                _context.Users.Add(_mapper.Map<Users>(usuario));
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

        public UserDTO GetUserById(int id)
        {         
            return _mapper.Map<UserDTO>(_context.Users.First(u => u.UserId == id));        
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

        public string ModifyUser(int id, UserViewModel usuarioModificado)
        {
            var usuario = _context.Users.First(u => u.UserId == id);
            var role = _context.RoleList.First(r => r.Id == usuarioModificado.RoleId);
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
        public string DeleteUser(int id)
        {
            var usuario = _context.Users.First(u => u.UserId == id);

            if (usuario != null)
            {
                _context.Users.Remove(usuario);
                _context.SaveChanges();
                return "Usuario eliminado exitosamente";
            }
            else
            {
                return "Usuario no encontrado";
            }
        }
    }
}
