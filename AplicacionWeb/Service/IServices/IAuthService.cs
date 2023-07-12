using Model.DTO;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAuthService
    {
        string CrearUsuario(UserViewModel User);
        string Login(AuthViewModel User);
    }
}
