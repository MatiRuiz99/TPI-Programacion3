using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappings.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Producto, ProductDTO>();
            CreateMap<ProductViewModel, Producto>();
        }
        
    }
}
