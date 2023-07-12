using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.DTO;
using Model.Helper;
using Model.Models;
using Model.ViewModel;
using Service.Helper;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly CafeteriaContext _context;
        private readonly AppSettings _appSettings;

        public AuthService(CafeteriaContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public string CrearUsuario(UserViewModel User)
        {
            if (string.IsNullOrEmpty(User.Email))
            {
                return "Ingrese un usuario";
            }

            Users? user = _context.Users.FirstOrDefault(x => x.Email == User.Email);

            if (user != null)
            {
                return "Usuario existente";
            }

            _context.Users.Add(new Users()
            {
                Name = User.Name,           
                Email = User.Email,
                Pass = User.Pass.GetSHA256(),
                RoleId = User.RoleId,
                
            });
            _context.SaveChanges();

            string response = GetToken(_context.Users.OrderBy(x => x.UserId).Last());

            return response;
        }

        public string Login(AuthViewModel User)
        {
            Users? user = _context.Users.FirstOrDefault(x => x.Email == User.Email && x.Pass == User.Pass.GetSHA256());

            if (user == null)
            {
                return string.Empty;
            }

            return GetToken(user);
        }

        private string GetToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, _context.RoleList.First(x => x.Id == user.RoleId).Authority)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
