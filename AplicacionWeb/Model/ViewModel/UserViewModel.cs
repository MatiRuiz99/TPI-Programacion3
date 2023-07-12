using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UserViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public int RoleId { get; set; } 
    }

    public class RoleListViewModel
    {
        public string Authority { get; set; } = string.Empty;
    }
}
