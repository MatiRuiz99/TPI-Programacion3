using AutoMapper;
using Model.DTO;
using Model.Models;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappings.Profiles
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<SalesHistory, SalesHistoryDTO>()
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<SalesViewModel, SalesHistory>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.IdProducto));
        }
    }
}
