using AutoMapper;
using PracticeApp.Domain.Models;
using PracticeApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeApp.Services.Utilities
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<OrderDto,Order>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        }
    }
}
