using Model.DTO;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService
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

        public Users GetUserById(int id)
        {
            var usuario = _context.Users.FirstOrDefault(u => u.UserId == id);

            return usuario;
        }

        public List<UserDTO> GetUserList()
        {
            return listadoUsuarios;
        }

        public List<UserDTO> ModifyUser(int id, UserDTO usuario)
        {
            var usuarioamodificar = listadoUsuarios.Where(x => x.UserId == id).First();
            usuarioamodificar.UserId = usuario.UserId;
            usuarioamodificar.Email = usuario.Email;
            usuarioamodificar.Name = usuario.Name;

            return listadoUsuarios;
        }
    }
}
