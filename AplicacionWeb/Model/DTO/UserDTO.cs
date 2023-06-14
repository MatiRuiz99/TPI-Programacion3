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
        public string? Email { get; set; }
        public string? Pass { get; set; }
        public string? RoleId { get; set;}

    }

    public class RoleList
    {
        public int Id { get; set;}
        public string? Authority { get; set; }
    }
}
