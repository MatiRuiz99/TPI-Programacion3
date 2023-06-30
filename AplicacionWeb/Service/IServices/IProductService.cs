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
    public interface IProductService
    {
        List<ProductDTO> GetProductList();
        ProductDTO GetProductById(int id);
        string CreateProduct(ProductViewModel producto);
        string ModifyProduct(int id, ProductViewModel productoAModificar);
        string DeleteProduct(int id);
    }
}
