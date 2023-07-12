using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class BestSellingProdDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int QuantitySold { get; set; }
    }
}
