﻿using AutoMapper;
using MyFirstMvc.Models.EFModels;
using MyFirstMvc.Models.ViewModels.Cart;

namespace MyFirstMvc.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 可以二個方向都寫, 但也可以直接用 ReverseMap() 來反轉,表示二個方向都要做
            //CreateMap<Category, CategoryDto>();
            //CreateMap<CategoryDto, Category>();
            CreateMap<Cart, CartVm>().ReverseMap();
            CreateMap<CartItem, CartItemsVm>().ReverseMap();
        }
    }
}