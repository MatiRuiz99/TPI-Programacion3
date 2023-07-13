using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using Service.Helper;
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


        public RoleListDTO CreateNewRole(RoleListViewModel newrole)
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
                return _mapper.Map<RoleListDTO>(role);
            }
            else
            {
                return null;
            }
        }

        public List<RoleListDTO> GetRoleList()
        {
            List<RoleList> roleList = _context.RoleList.ToList();
            List<RoleListDTO> response = _mapper.Map<List<RoleListDTO>>(roleList);
            return response;

        }

        public UserDTO GetUserById(int id)
        {
            var response = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (response  == null)
            {
                return null;
            }
            else { 
            return _mapper.Map<UserDTO>(response);
            }
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
                usuario.Pass = usuarioModificado.Pass.GetSHA256();
                usuario.RoleId = role.Id;

                _context.SaveChanges();
                return "User modified successfully";
            }
                else if (usuario == null)
                {
                    return "User not found";
                }
            else
            {
                return "Error modifying user (non-existent role)";
            }

            }
        public string DeleteUser(int id)
        {
            var usuario = _context.Users.First(u => u.UserId == id);

            if (usuario != null)
            {
                _context.Users.Remove(usuario);
                _context.SaveChanges();
                return "User deleted successfully";
            }
            else
            {
                return "User not found";
            }
        }
    }
}
