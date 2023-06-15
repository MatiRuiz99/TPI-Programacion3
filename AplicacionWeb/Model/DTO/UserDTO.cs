using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int RoleId { get; set;}

    }

    public class RoleListDTO
    {
        public int Id { get; set;}
        public string Authority { get; set; } = string.Empty;
    }
}
