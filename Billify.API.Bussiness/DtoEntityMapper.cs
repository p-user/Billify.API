using AutoMapper;
using Billify.API.Common.Dtos;
using Billify.API.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billify.API.Bussiness
{
    public class DtoEntityMapper:Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<ProductDto, Product>().ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Product, ProductDto>().ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<ClientDto, Client>().ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Client, ClientDto>().ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<BillDto, Bill>()
                .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Bill, BillDto>()
                .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<BillDto, Bill_Product>();
            //CreateMap<Bill_Product, BillDto>()
            //    .ForMember(dest => dest.bill,
            //    opt => opt.MapFrom(src => src.Id));
        }
        
  
          
    }
}
