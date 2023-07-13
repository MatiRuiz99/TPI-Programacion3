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
        
        RoleListDTO CreateNewRole(RoleListViewModel newrole);
        List<RoleListDTO> GetRoleList();
        UserDTO GetUserById(int id);
        List<UserxRoleDTO> GetUserList();
        string ModifyUser(int id, UserViewModel usuarioModificado);
        string DeleteUser(int id);

    }
}
