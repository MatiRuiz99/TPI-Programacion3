using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        static readonly List<UserDTO> listadoUsuarios = new List<UserDTO>();
        public List<UserDTO> CreateUsuario(UserDTO usuario)
        {
            listadoUsuarios.Add(usuario);

            return listadoUsuarios;
        }

        public UserDTO GetUserById(int id)
        {
            UserDTO producto = listadoUsuarios.Where(x => x.UserId == id).First();

            return producto;
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
            usuarioamodificar.Pass = usuario.Pass;

            return listadoUsuarios;  
        }
    }
}
