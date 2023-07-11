using AutoMapper;
using Service.Mappings.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mappings
{
    public class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ProductProfile>();
            });
            
            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
    }
}
