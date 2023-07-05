using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class SalesHistoryDTO
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public int IdProducto { get; set; }
        public decimal? Price { get; set; }
        public DateTime date { get; set; }

    }

    public class SalesHistoryReturn
    {
        public string User { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string Prod { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public DateTime? date { get; set; }

    }
}
