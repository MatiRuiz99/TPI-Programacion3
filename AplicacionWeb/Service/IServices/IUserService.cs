using Model.DTO;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IUserService
    {
        string CreateUsuario(UserViewModel usuario);
        string CreateNewRole(RoleListViewModel newrole);
        List<RoleList> GetRoleList();
        Users GetUserById(int id);
        List<Users> GetUserList();
        string ModifyUser(int id, UserDTO usuarioModificado);
    }
}
