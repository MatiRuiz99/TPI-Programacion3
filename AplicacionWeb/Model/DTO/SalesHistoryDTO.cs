using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class SalesHistoryDTO
    {
        public int UserId { get; set; }
        public int IdProducto { get; set; }
        public decimal Price { get; set; }
        public DateTime date { get; set; }

    }
}
